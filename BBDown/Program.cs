using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static BBDown.Core.Entity.Entity;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using BBDown.Core;
using BBDown.Core.Util;
using System.Text.Json.Serialization;
using BBDown.Core.Entity;
using BBDown.Core.DRM;
using System.Diagnostics;
using Spectre.Console.Cli;
using BBDown.Commands;

namespace BBDown;

partial class Program
{
    private static readonly string BACKUP_HOST = "upos-sz-mirrorcoso1.bilivideo.com";
    public static string SinglePageDefaultSavePath { get; set; } = "<videoTitle>";
    public static string MultiPageDefaultSavePath { get; set; } = "<videoTitle>/[P<pageNumberWithZero>]<pageTitle>";

    public static readonly string APP_DIR = Path.GetDirectoryName(Environment.ProcessPath)!;

    private static string FormatTimeStamp(long ts, string format)
    {
        try
        {
            return ts == 0 ? "null" : DateTimeOffset.FromUnixTimeSeconds(ts).ToLocalTime().ToString(format);
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException or FormatException)
        {
            Logger.LogError(Localizer.GetString("date_format_err", ex.Message));
            return ts.ToString();
        }
    }

    [JsonSerializable(typeof(MyOption))]
    [JsonSerializable(typeof(ServeRequestOptions))]
    partial class MyOptionJsonContext : JsonSerializerContext { }

    private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        Logger.LogWarn(Localizer.GetString("force_exit"));
        try
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            if (!OperatingSystem.IsWindows())
                System.Diagnostics.Process.Start("stty", "echo");
        }
        catch { }
        Environment.Exit(0);
    }

    public static async Task<int> Main(params string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CancelKeyPress += Console_CancelKeyPress;

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;
        Console.Write($"BBDown version {ver.Major}.{ver.Minor}.{ver.Build}, Bilibili Downloader.\r\n");
        Console.ResetColor();
        var mergedArgs = BBDownConfigParser.MergeWithConfig(args).ToArray();

        // Parse locale parameter
        for (int i = 0; i < mergedArgs.Length; i++)
        {
            if (mergedArgs[i].StartsWith("--locale=", StringComparison.OrdinalIgnoreCase))
            {
                Localizer.CultureName = mergedArgs[i].Substring(9);
            }
            else if (mergedArgs[i].Equals("--locale", StringComparison.OrdinalIgnoreCase) && i + 1 < mergedArgs.Length)
            {
                Localizer.CultureName = mergedArgs[i + 1];
            }
        }

        Console.Write(Localizer.GetString("welcome_msg") + "\r\nhttps://github.com/AliverAnme/BBDown/issues\r\n");
        Console.WriteLine();

        if (mergedArgs.Contains("--debug"))
        {
            Config.Apply(Config.Current with { DebugLog = true });
        }

        var services = new ServiceCollection();
        var registrar = new TypeRegistrar(services);
        var app = new CommandApp<DefaultCommand>(registrar);
        app.Configure(config =>
        {
            config.SetApplicationName("BBDown");
            config.SetApplicationVersion($"{ver.Major}.{ver.Minor}.{ver.Build}");
            config.SetExceptionHandler((ex, resolver) =>
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                var msg = Config.Current.DebugLog ? ex.ToString() : ex.Message;
                Console.Error.WriteLine(msg);
                Console.Error.WriteLine(Localizer.GetString("upgrade_retry"));
                Console.ResetColor();
                try { Console.CursorVisible = true; } catch { }
                return 1;
            });

            config.AddCommand<LoginCommand>("login")
                  .WithDescription(Localizer.GetString("cmd_login"));
            config.AddCommand<LoginTVCommand>("logintv")
                  .WithDescription(Localizer.GetString("cmd_logintv"));
            config.AddCommand<ServeCommand>("serve")
                  .WithDescription(Localizer.GetString("cmd_serve"));
        });

        return await app.RunAsync(mergedArgs);
    }

    internal static void StartServer(string? listenUrl, int maxConcurrent = 3)
    {
        var defaultListenUrl = "http://0.0.0.0:23333";
        Logger.LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "bbdown-api.log");
        var server = new BBDownApiServer(maxConcurrent);
        server.SetUpServer();
        server.Run(string.IsNullOrEmpty(listenUrl) ? defaultListenUrl : listenUrl);
    }

    internal static async Task DoWorkAsync(MyOption myOption, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var (encodingPriority, dfnPriority, firstEncoding, downloadDanmaku, downloadDanmakuFormats,
            input, savePathFormat, lang, aidOri, delay) = SetUpWork(myOption);
        var (fetchedAid, vInfo, apiType) = await GetVideoInfoAsync(myOption, aidOri, input);
        await DownloadPagesAsync(myOption, vInfo, encodingPriority, dfnPriority, firstEncoding, downloadDanmaku, downloadDanmakuFormats,
            input, savePathFormat, lang, fetchedAid, delay, apiType, cancellationToken: cancellationToken);
    }

}
