using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace BBDown;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)]
public class MyOption : CommandSettings
{
    [CommandArgument(0, "<URL>")]
    [LocalizedDescription("opt_url")]
    public string Url { get; set; } = "";

    [CommandOption("-t|--use-tv-api")]
    [LocalizedDescription("opt_use_tv_api")]
    public bool UseTvApi { get; set; }

    [CommandOption("-a|--use-app-api")]
    [LocalizedDescription("opt_use_app_api")]
    public bool UseAppApi { get; set; }

    [CommandOption("--use-intl-api")]
    [LocalizedDescription("opt_use_intl_api")]
    public bool UseIntlApi { get; set; }

    [CommandOption("--use-mp4box")]
    [LocalizedDescription("opt_use_mp4box")]
    public bool UseMP4box { get; set; }

    [CommandOption("-e|--encoding-priority")]
    [LocalizedDescription("opt_encoding_priority")]
    public string? EncodingPriority { get; set; }

    [CommandOption("-q|--dfn-priority")]
    [LocalizedDescription("opt_dfn_priority")]
    public string? DfnPriority { get; set; }

    [CommandOption("-I|--only-show-info")]
    [LocalizedDescription("opt_only_show_info")]
    public bool OnlyShowInfo { get; set; }

    [CommandOption("--show-all")]
    [LocalizedDescription("opt_show_all")]
    public bool ShowAll { get; set; }

    [CommandOption("--use-aria2c")]
    [LocalizedDescription("opt_use_aria2c")]
    public bool UseAria2c { get; set; }

    [CommandOption("-i|--interactive")]
    [LocalizedDescription("opt_interactive")]
    public bool Interactive { get; set; }

    [CommandOption("--hide-streams")]
    [LocalizedDescription("opt_hide_streams")]
    public bool HideStreams { get; set; }

    [CommandOption("--multi-thread")]
    [LocalizedDescription("opt_multi_thread")]
    public bool MultiThread { get; set; } = true;

    [CommandOption("--simply-mux")]
    [LocalizedDescription("opt_simply_mux")]
    public bool SimplyMux { get; set; } = false;

    [CommandOption("--video-only")]
    [LocalizedDescription("opt_video_only")]
    public bool VideoOnly { get; set; }

    [CommandOption("--audio-only")]
    [LocalizedDescription("opt_audio_only")]
    public bool AudioOnly { get; set; }

    [CommandOption("--danmaku-only")]
    [LocalizedDescription("opt_danmaku_only")]
    public bool DanmakuOnly { get; set; }

    [CommandOption("--cover-only")]
    [LocalizedDescription("opt_cover_only")]
    public bool CoverOnly { get; set; }

    [CommandOption("--sub-only")]
    [LocalizedDescription("opt_sub_only")]
    public bool SubOnly { get; set; }

    [CommandOption("--debug")]
    [LocalizedDescription("opt_debug")]
    public bool Debug { get; set; }

    [CommandOption("--skip-mux")]
    [LocalizedDescription("opt_skip_mux")]
    public bool SkipMux { get; set; }

    [CommandOption("--insecure")]
    [LocalizedDescription("opt_insecure")]
    public bool Insecure { get; set; }

    [CommandOption("--decrypt-drm")]
    [LocalizedDescription("opt_decrypt_drm")]
    public bool DecryptDrm { get; set; }

    [CommandOption("--key")]
    [LocalizedDescription("opt_drm_key")]
    public string? DrmKeyHex { get; set; }

    [CommandOption("--kid")]
    [LocalizedDescription("opt_drm_kid")]
    public string? DrmKidHex { get; set; }

    [CommandOption("--mp4decrypt-path")]
    [LocalizedDescription("opt_mp4decrypt_path")]
    public string Mp4decryptPath { get; set; } = "";

    [CommandOption("--wvd-path")]
    [LocalizedDescription("opt_wvd_path")]
    public string WvdPath { get; set; } = "";

    [CommandOption("--skip-subtitle")]
    [LocalizedDescription("opt_skip_subtitle")]
    public bool SkipSubtitle { get; set; }

    [CommandOption("--skip-cover")]
    [LocalizedDescription("opt_skip_cover")]
    public bool SkipCover { get; set; }

    [CommandOption("--force-http")]
    [LocalizedDescription("opt_force_http")]
    public bool ForceHttp { get; set; } = true;

    [CommandOption("-d|--download-danmaku")]
    [LocalizedDescription("opt_download_danmaku")]
    public bool DownloadDanmaku { get; set; } = false;

    [CommandOption("--download-danmaku-formats")]
    [LocalizedDescription("opt_download_danmaku_formats")]
    public string? DownloadDanmakuFormats { get; set; }

    [CommandOption("--skip-ai")]
    [LocalizedDescription("opt_skip_ai")]
    public bool SkipAi { get; set; } = true;

    [CommandOption("--video-ascending")]
    [LocalizedDescription("opt_video_ascending")]
    public bool VideoAscending { get; set; } = false;

    [CommandOption("--audio-ascending")]
    [LocalizedDescription("opt_audio_ascending")]
    public bool AudioAscending { get; set; } = false;

    [CommandOption("--allow-pcdn")]
    [LocalizedDescription("opt_allow_pcdn")]
    public bool AllowPcdn { get; set; } = false;

    [CommandOption("-F|--file-pattern")]
    [LocalizedDescription("opt_file_pattern")]
    public string FilePattern { get; set; } = "";

    [CommandOption("-M|--multi-file-pattern")]
    [LocalizedDescription("opt_multi_file_pattern")]
    public string MultiFilePattern { get; set; } = "";

    [CommandOption("-p|--select-page")]
    [LocalizedDescription("opt_select_page")]
    public string SelectPage { get; set; } = "";

    [CommandOption("--language")]
    [LocalizedDescription("opt_language")]
    public string Language { get; set; } = "";

    [CommandOption("-u|--user-agent")]
    [LocalizedDescription("opt_user_agent")]
    public string UserAgent { get; set; } = "";

    [CommandOption("-c|--cookie")]
    [LocalizedDescription("opt_cookie")]
    public string Cookie { get; set; } = "";

    [CommandOption("--access-token")]
    [LocalizedDescription("opt_access_token")]
    public string AccessToken { get; set; } = "";

    [CommandOption("--aria2c-args")]
    [LocalizedDescription("opt_aria2c_args")]
    public string Aria2cArgs { get; set; } = "";

    [CommandOption("--work-dir")]
    [LocalizedDescription("opt_work_dir")]
    public string WorkDir { get; set; } = "";

    [CommandOption("--ffmpeg-path")]
    [LocalizedDescription("opt_ffmpeg_path")]
    public string FFmpegPath { get; set; } = "";

    [CommandOption("--mp4box-path")]
    [LocalizedDescription("opt_mp4box_path")]
    public string Mp4boxPath { get; set; } = "";

    [CommandOption("--aria2c-path")]
    [LocalizedDescription("opt_aria2c_path")]
    public string Aria2cPath { get; set; } = "";

    [CommandOption("--upos-host")]
    [LocalizedDescription("opt_upos_host")]
    public string UposHost { get; set; } = "";

    [CommandOption("--force-replace-host")]
    [LocalizedDescription("opt_force_replace_host")]
    public bool ForceReplaceHost { get; set; } = true;

    [CommandOption("--save-archives-to-file")]
    [LocalizedDescription("opt_save_archives")]
    public bool SaveArchivesToFile { get; set; } = false;

    [CommandOption("--delay-per-page")]
    [LocalizedDescription("opt_delay_per_page")]
    public int DelayPerPage { get; set; } = 0;

    [CommandOption("--muxer-timeout")]
    [LocalizedDescription("opt_muxer_timeout")]
    public int MuxerTimeout { get; set; } = 30;

    [CommandOption("--retry-count")]
    [LocalizedDescription("opt_retry_count")]
    public int RetryCount { get; set; } = 3;

    [CommandOption("--retry-delay")]
    [LocalizedDescription("opt_retry_delay")]
    public int RetryDelay { get; set; } = 3000;

    [CommandOption("--thread-segment-size")]
    [LocalizedDescription("opt_thread_segment")]
    public int ThreadSegmentSize { get; set; } = 20;

    [CommandOption("--host")]
    [LocalizedDescription("opt_host")]
    public string Host { get; set; } = "api.bilibili.com";

    [CommandOption("--ep-host")]
    [LocalizedDescription("opt_ep_host")]
    public string EpHost { get; set; } = "api.bilibili.com";

    [CommandOption("--tv-host")]
    [LocalizedDescription("opt_tv_host")]
    public string TvHost { get; set; } = "api.snm0516.aisee.tv";

    [CommandOption("--area")]
    [LocalizedDescription("opt_area")]
    public string Area { get; set; } = "";

    [CommandOption("--config-file")]
    [LocalizedDescription("opt_config_file")]
    public string? ConfigFile { get; set; }

    [CommandOption("--locale")]
    [LocalizedDescription("opt_locale")]
    public string Locale { get; set; } = "";

    // 以下仅为兼容旧版本命令行，不建议使用
    [CommandOption("--aria2c-proxy", IsHidden = true)]
    public string Aria2cProxy { get; set; } = "";

    [CommandOption("--only-hevc", IsHidden = true)]
    public bool OnlyHevc { get; set; }

    [CommandOption("--only-avc", IsHidden = true)]
    public bool OnlyAvc { get; set; }

    [CommandOption("--only-av1", IsHidden = true)]
    public bool OnlyAv1 { get; set; }

    [CommandOption("--add-dfn-subfix", IsHidden = true)]
    public bool AddDfnSuffix { get; set; }

    [CommandOption("--no-padding-page-num", IsHidden = true)]
    public bool NoPaddingPageNum { get; set; }

    [CommandOption("--bandwith-ascending", IsHidden = true)]
    public bool BandwidthAscending { get; set; }
}
