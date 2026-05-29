using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static BBDown.Core.Entity.Entity;
using BBDown.Core;
using BBDown.Core.Entity;

using BBDown.Core.Util;
using System.Text.Json;
namespace BBDown;

internal partial class Program
{
    public static (Dictionary<string, byte> encodingPriority, Dictionary<string, int> dfnPriority, string? firstEncoding,
        bool downloadDanmaku, BBDownDanmakuFormat[] downloadDanmakuFormats, string input, string savePathFormat, string lang, string aidOri, int delay)
        SetUpWork(MyOption myOption)
    {
        //处理废弃选项
        HandleDeprecatedOptions(myOption);

        //处理冲突选项
        HandleConflictingOptions(myOption);

        //寻找并设置所需的二进制文件路径
        FindBinaries(myOption);

        //切换工作目录
        ChangeWorkingDir(myOption);

        //解析优先级
        var encodingPriority = ParseEncodingPriority(myOption, out var firstEncoding);
        var dfnPriority = ParseDfnPriority(myOption);

        //优先使用用户设置的UA
        HTTPUtil.UserAgent = string.IsNullOrEmpty(myOption.UserAgent) ? HTTPUtil.UserAgent : myOption.UserAgent;

        bool downloadDanmaku = myOption.DownloadDanmaku || myOption.DanmakuOnly;
        BBDownDanmakuFormat[] downloadDanmakuFormats = ParseDownloadDanmakuFormats(myOption);

        string input = myOption.Url;
        string savePathFormat = myOption.FilePattern;
        string lang = myOption.Language;
        string aidOri = ""; //原始aid
        int delay = myOption.DelayPerPage;
        Config.Apply(new AppSettings(
            Cookie: myOption.Cookie,
            Token: myOption.AccessToken.Replace("access_token=", ""),
            DebugLog: myOption.Debug,
            Host: myOption.Host,
            EpHost: myOption.EpHost,
            TvHost: myOption.TvHost,
            Area: myOption.Area,
            SkipSslCheck: myOption.Insecure,
            MuxerTimeoutMinutes: myOption.MuxerTimeout,
            MaxRetryCount: myOption.RetryCount,
            RetryDelayMs: myOption.RetryDelay,
            ThreadSegmentSizeMb: myOption.ThreadSegmentSize
        ));

        Logger.LogDebug("AppDirectory: {0}", APP_DIR);
        if (Config.Current.DebugLog)
        {
            var savedCookie = myOption.Cookie;
            var savedToken = myOption.AccessToken;
            myOption.Cookie = string.IsNullOrEmpty(savedCookie) ? "" : "***";
            myOption.AccessToken = string.IsNullOrEmpty(savedToken) ? "" : "***";
            Logger.LogDebug("运行参数：{0}", JsonSerializer.Serialize(myOption, MyOptionJsonContext.Default.MyOption));
            myOption.Cookie = savedCookie;
            myOption.AccessToken = savedToken;
        }
        return (encodingPriority, dfnPriority, firstEncoding, downloadDanmaku, downloadDanmakuFormats, input, savePathFormat, lang, aidOri, delay);
    }

    public static async Task<(string fetchedAid, VInfo vInfo, string apiType)> GetVideoInfoAsync(MyOption myOption, string aidOri, string input)
    {
        // 加载认证信息
        LoadCredentials(myOption);

        // 检测是否登录了账号
        if (myOption is { UseIntlApi: false, UseTvApi: false } && Config.Current.Area == "")
        {
            Logger.Log(Localizer.GetString("check_login"));
            var (isLoggedIn, cookieExpired) = await BBDownUtil.CheckLoginWithDetails(Config.Current.Cookie);
            if (!isLoggedIn)
            {
                if (cookieExpired)
                {
                    Logger.LogWarn("========================================");
                    Logger.LogWarn(Localizer.GetString("cookie_expired_warn1"));
                    Logger.LogWarn(Localizer.GetString("cookie_expired_warn2"));
                    Logger.LogWarn(Localizer.GetString("cookie_expired_warn3"));
                    Logger.LogWarn("========================================");
                }
                else
                {
                    Logger.LogWarn("========================================");
                    Logger.LogWarn(Localizer.GetString("not_logged_in_warn1"));
                    Logger.LogWarn(Localizer.GetString("not_logged_in_warn2"));
                    Logger.LogWarn(Localizer.GetString("not_logged_in_warn3"));
                    Logger.LogWarn("========================================");
                }
            }
        }

        Logger.Log(Localizer.GetString("get_aid"));
        aidOri = await UrlResolver.ResolveAsync(input);
        Logger.Log(Localizer.GetString("get_aid_end", aidOri));

        if (string.IsNullOrEmpty(aidOri))
        {
            throw new ArgumentException(Localizer.GetString("invalid_url_id"));
        }

        Logger.Log(Localizer.GetString("get_video_info"));
        IFetcher fetcher = FetcherFactory.CreateFetcher(aidOri, myOption.UseIntlApi);
        VInfo? vInfo = null;

        // 只输入 EP/SS 时优先按番剧查找，如果找不到则尝试按课程查找
        try
        {
            vInfo = await fetcher.FetchAsync(aidOri);
        }
        catch (Exception e) when (e is KeyNotFoundException or InvalidOperationException)
        {
            // B站返回非番剧JSON结构（可能是课程），尝试按课程查找
            if (aidOri.StartsWith("cheese:")) throw; // 已经按课程查找过，不再重复尝试

            Logger.LogWarn(Localizer.GetString("ep_ss_not_found_bangumi"));

            aidOri = aidOri.Replace("ep", "cheese");
            Logger.Log(Localizer.GetString("new_aid", aidOri));

            if (string.IsNullOrEmpty(aidOri))
            {
                throw new ArgumentException(Localizer.GetString("get_video_info_err"));
            }

            Logger.Log(Localizer.GetString("get_video_info"));
            fetcher = FetcherFactory.CreateFetcher(aidOri, myOption.UseIntlApi);
            vInfo = await fetcher.FetchAsync(aidOri);
        }

        string title = vInfo.Title;
        long pubTime = vInfo.PubTime;
        Logger.LogColor(Localizer.GetString("video_title", title));
        if (pubTime != 0)
        {
            Logger.Log(Localizer.GetString("pub_time", FormatTimeStamp(pubTime, "yyyy-MM-dd HH:mm:ss zzz")));
        }
        var bvid = vInfo.PagesInfo.FirstOrDefault()?.bvid;
        if (!string.IsNullOrEmpty(bvid) && !myOption.UseIntlApi)
        {
            Logger.Log(Localizer.GetString("video_url", $"https://www.bilibili.com/video/{bvid}/"));
        }
        var mid = vInfo.PagesInfo.FirstOrDefault(p => !string.IsNullOrEmpty(p.ownerMid))?.ownerMid;
        if (!string.IsNullOrEmpty(mid))
        {
            Logger.Log(Localizer.GetString("up_space", $"https://space.bilibili.com/{mid}"));
        }

        if (vInfo.IsSteinGate && myOption.UseTvApi)
        {
            Logger.Log(Localizer.GetString("interactive_video_warn"));
            myOption.UseTvApi = false;
        }
        string apiType = myOption.UseTvApi ? "TV" : (myOption.UseAppApi ? "APP" : (myOption.UseIntlApi ? "INTL" : "WEB"));

        //打印分P信息
        List<Page> pagesInfo = vInfo.PagesInfo;
        bool more = false;
        foreach (Page p in pagesInfo)
        {
            if (!myOption.ShowAll)
            {
                if (more && p.index != pagesInfo.Count) continue;
                if (!more && p.index > 5)
                {
                    Logger.Log("......");
                    more = true;
                    continue;
                }
            }

            Logger.Log($"P{p.index}: [{p.cid}] [{p.title}] [{BBDownUtil.FormatTime(p.dur)}]");
        }
        return (aidOri, vInfo, apiType);
    }

}
