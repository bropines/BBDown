using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static BBDown.Core.Entity.Entity;
using System.Linq;
using BBDown.Core;
using BBDown.Core.Entity;

using BBDown.Core.Util;
using System.Text.Json;
namespace BBDown;

internal partial class Program
{
    private static void PrintAllTracksInfo(ParsedResult parsedResult, int pageDur, bool onlyShowInfo)
    {
        if (parsedResult.BackgroundAudioTracks.Any() && parsedResult.RoleAudioList.Any())
        {
            Logger.Log(Localizer.GetString("count_bg_audio", parsedResult.BackgroundAudioTracks.Count));
            int index = 0;
            foreach (var a in parsedResult.BackgroundAudioTracks)
            {
                int pDur = pageDur == 0 ? a.dur : pageDur;
                Logger.LogColor($"{index++}. [{a.codecs}] [{a.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(pDur * a.bandwidth * 1024 / 8)}]", false);
            }
            Logger.Log(Localizer.GetString("count_role_audio", parsedResult.RoleAudioList.Count, parsedResult.RoleAudioList[0].audio.Count));
            index = 0;
            foreach (var a in parsedResult.RoleAudioList[0].audio)
            {
                int pDur = pageDur == 0 ? a.dur : pageDur;
                Logger.LogColor($"{index++}. [{a.codecs}] [{a.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(pDur * a.bandwidth * 1024 / 8)}]", false);
            }
        }
        //展示所有的音视频流信息
        if (parsedResult.VideoTracks.Any())
        {
            Logger.Log(Localizer.GetString("count_video", parsedResult.VideoTracks.Count));
            int index = 0;
            foreach (var v in parsedResult.VideoTracks)
            {
                int pDur = pageDur == 0 ? v.dur : pageDur;
                var size = v.size > 0 ? v.size : pDur * v.bandwidth * 1024 / 8;
                Logger.LogColor($"{index++}. [{v.dfn}] [{v.res}] [{v.codecs}] [{v.fps}] [{v.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(size)}]".Replace("[] ", ""), false);
                if (onlyShowInfo) Console.WriteLine(v.baseUrl);
            }
        }
        if (parsedResult.AudioTracks.Any())
        {
            Logger.Log(Localizer.GetString("count_audio", parsedResult.AudioTracks.Count));
            int index = 0;
            foreach (var a in parsedResult.AudioTracks)
            {
                int pDur = pageDur == 0 ? a.dur : pageDur;
                Logger.LogColor($"{index++}. [{a.codecs}] [{a.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(pDur * a.bandwidth * 1024 / 8)}]", false);
                if (onlyShowInfo) Console.WriteLine(a.baseUrl);
            }
        }
    }

    private static void PrintSelectedTrackInfo(Video? selectedVideo, Audio? selectedAudio, int pageDur)
    {
        if (selectedVideo != null)
        {
            int pDur = pageDur == 0 ? selectedVideo.dur : pageDur;
            var size = selectedVideo.size > 0 ? selectedVideo.size : pDur * selectedVideo.bandwidth * 1024 / 8;
            Logger.LogColor($"[{Localizer.GetString("video_stream")}] [{selectedVideo.dfn}] [{selectedVideo.res}] [{selectedVideo.codecs}] [{selectedVideo.fps}] [{selectedVideo.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(size)}]".Replace("[] ", ""), false);
        }
        if (selectedAudio != null)
        {
            int pDur = pageDur == 0 ? selectedAudio.dur : pageDur;
            Logger.LogColor($"[{Localizer.GetString("audio_stream")}] [{selectedAudio.codecs}] [{selectedAudio.bandwidth} kbps] [~{BBDownUtil.FormatFileSize(pDur * selectedAudio.bandwidth * 1024 / 8)}]", false);
        }
    }

    /// <summary>
    /// 引导用户进行手动选择轨道
    /// </summary>
    /// <param name="parsedResult"></param>
    /// <param name="vIndex"></param>
    /// <param name="aIndex"></param>
    private static int ReadIntSafe()
    {
        if (!int.TryParse(Console.ReadLine(), out var val))
            return 0;
        return val;
    }

    private static void SelectTrackManually(ParsedResult parsedResult, ref int vIndex, ref int aIndex)
    {
        if (parsedResult.VideoTracks.Any())
        {
            Logger.Log(Localizer.GetString("select_video_prompt"), false);
            Console.ForegroundColor = ConsoleColor.Cyan;
            vIndex = ReadIntSafe();
            if (vIndex > parsedResult.VideoTracks.Count || vIndex < 0) vIndex = 0;
            Console.ResetColor();
        }
        if (parsedResult.AudioTracks.Any())
        {
            Logger.Log(Localizer.GetString("select_audio_prompt"), false);
            Console.ForegroundColor = ConsoleColor.Cyan;
            aIndex = ReadIntSafe();
            if (aIndex > parsedResult.AudioTracks.Count || aIndex < 0) aIndex = 0;
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 下载轨道
    /// </summary>
    /// <returns></returns>
    private static async Task DownloadTrackAsync(string url, string destPath, BBDownDownloadUtil.DownloadConfig downloadConfig, bool video)
    {
        if (downloadConfig.MultiThread && !url.Contains("-cmcc-"))
        {
            await BBDownDownloadUtil.MultiThreadDownloadFileAsync(url, destPath, downloadConfig);
            Logger.Log(Localizer.GetString("merge_clips", video ? Localizer.GetString("video_stream") : Localizer.GetString("audio_stream")));
            BBDownUtil.CombineMultipleFilesIntoSingleFile(BBDownUtil.GetFiles(Path.GetDirectoryName(destPath)!, $".{(video ? "v" : "a")}clip"), destPath);
            Logger.Log(Localizer.GetString("clean_clips"));
            foreach (var file in new DirectoryInfo(Path.GetDirectoryName(destPath)!).EnumerateFiles("*.?clip")) file.Delete();
        }
        else
        {
            if (downloadConfig.MultiThread && url.Contains("-cmcc-"))
            {
                Logger.LogWarn(Localizer.GetString("cmcc_multithread_disabled"));
                downloadConfig.ForceHttp = false;
            }
            await BBDownDownloadUtil.DownloadFileAsync(url, destPath, downloadConfig);
        }
    }
}
