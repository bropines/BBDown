using System;
using System.Collections.Generic;
using System.Globalization;

namespace BBDown.Core.Util;

public static class Localizer
{
    private static string? _cultureName;

    public static string CultureName
    {
        get => _cultureName ??= GetDefaultCulture();
        set => _cultureName = value;
    }

    private static string GetDefaultCulture()
    {
        // 1. Check environment variable BBDOWN_LOCALE
        var envLocale = Environment.GetEnvironmentVariable("BBDOWN_LOCALE");
        if (!string.IsNullOrEmpty(envLocale))
        {
            if (envLocale.StartsWith("ru", StringComparison.OrdinalIgnoreCase)) return "ru";
            if (envLocale.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return "zh";
            return "en";
        }

        // 2. Check system culture
        try
        {
            var sysCulture = CultureInfo.CurrentUICulture.Name;
            if (sysCulture.StartsWith("ru", StringComparison.OrdinalIgnoreCase)) return "ru";
            if (sysCulture.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return "zh";
        }
        catch { }

        return "en";
    }

    private static readonly Dictionary<string, Dictionary<string, string>> Translations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["zh"] = new(StringComparer.OrdinalIgnoreCase)
        {
            // CLI Options & Arguments
            ["opt_url"] = "视频地址 或 av|bv|BV|ep|ss",
            ["opt_use_tv_api"] = "使用TV端解析模式",
            ["opt_use_app_api"] = "使用APP端解析模式",
            ["opt_use_intl_api"] = "使用国际版(东南亚视频)解析模式",
            ["opt_use_mp4box"] = "使用MP4Box来混流",
            ["opt_encoding_priority"] = "视频及音频编码的选择优先级, 用逗号分割 例: \"hevc,av1,avc,flac,eac3,m4a\"",
            ["opt_dfn_priority"] = "画质优先级,用逗号分隔 例: \"8K 超高清, 1080P 高码率, HDR 真彩, 杜比视界\"",
            ["opt_only_show_info"] = "仅解析而不进行下载",
            ["opt_show_all"] = "展示所有分P标题",
            ["opt_use_aria2c"] = "调用aria2c进行下载(你需要自行准备好二进制可执行文件)",
            ["opt_interactive"] = "交互式选择清晰度",
            ["opt_hide_streams"] = "不要显示所有可用音视频流",
            ["opt_multi_thread"] = "使用多线程下载(默认开启)",
            ["opt_simply_mux"] = "精简混流，不增加描述、作者等信息",
            ["opt_video_only"] = "仅下载视频",
            ["opt_audio_only"] = "仅下载音频",
            ["opt_danmaku_only"] = "仅下载弹幕",
            ["opt_cover_only"] = "仅下载封面",
            ["opt_sub_only"] = "仅下载字幕",
            ["opt_debug"] = "输出调试日志",
            ["opt_skip_mux"] = "跳过混流步骤",
            ["opt_insecure"] = "跳过SSL证书验证(仅用于抓包/代理场景)",
            ["opt_decrypt_drm"] = "尝试解密DRM保护视频",
            ["opt_drm_key"] = "DRM解密密钥 (hex)",
            ["opt_drm_kid"] = "DRM密钥ID (hex)",
            ["opt_mp4decrypt_path"] = "设置mp4decrypt的路径",
            ["opt_wvd_path"] = "设置device.wvd的路径",
            ["opt_skip_subtitle"] = "跳过字幕下载",
            ["opt_skip_cover"] = "跳过封面下载",
            ["opt_force_http"] = "下载音视频时强制使用HTTP协议替换HTTPS(默认开启)",
            ["opt_download_danmaku"] = "下载弹幕",
            ["opt_download_danmaku_formats"] = "指定需下载的弹幕格式, 用逗号分隔",
            ["opt_skip_ai"] = "跳过AI字幕下载(默认开启)",
            ["opt_video_ascending"] = "视频升序(最小体积优先)",
            ["opt_audio_ascending"] = "音频升序(最小体积优先)",
            ["opt_allow_pcdn"] = "不替换PCDN域名, 仅在正常情况与--upos-host均无法下载时使用",
            ["opt_file_pattern"] = "使用内置变量自定义单P存储文件名",
            ["opt_multi_file_pattern"] = "使用内置变量自定义多P存储文件名",
            ["opt_select_page"] = "选择指定分p或分p范围: (-p 8 或 -p 1,2 或 -p 3-5 或 -p ALL 或 -p LAST 或 -p 3,5,LATEST)",
            ["opt_language"] = "设置混流的音频语言(代码), 如chi, jpn等",
            ["opt_user_agent"] = "指定user-agent, 否则使用随机user-agent",
            ["opt_cookie"] = "设置字符串cookie用以下载网页接口的会员内容",
            ["opt_access_token"] = "设置access_token用以下载TV/APP接口的会员内容",
            ["opt_aria2c_args"] = "调用aria2c的附加参数",
            ["opt_work_dir"] = "设置程序的工作目录",
            ["opt_ffmpeg_path"] = "设置ffmpeg的路径",
            ["opt_mp4box_path"] = "设置mp4box的路径",
            ["opt_aria2c_path"] = "设置aria2c的路径",
            ["opt_upos_host"] = "自定义upos服务器",
            ["opt_force_replace_host"] = "强制替换下载服务器host(默认开启)",
            ["opt_save_archives"] = "将下载过的视频记录到本地文件中, 用于后续跳过下载同个视频",
            ["opt_delay_per_page"] = "设置下载合集分P之间的下载间隔时间(单位: 秒, 默认无间隔)",
            ["opt_muxer_timeout"] = "混流超时时长(分钟, 默认30)",
            ["opt_retry_count"] = "网络请求失败后的重试次数(默认3)",
            ["opt_retry_delay"] = "重试间隔基础毫秒数(默认3000)",
            ["opt_thread_segment"] = "多线程下载时分片大小(MB, 默认20)",
            ["opt_host"] = "指定BiliPlus host",
            ["opt_ep_host"] = "指定BiliPlus EP host",
            ["opt_tv_host"] = "自定义tv端接口请求Host",
            ["opt_area"] = "(hk|tw|th) 使用BiliPlus时必选, 指定BiliPlus area",
            ["opt_config_file"] = "读取指定的BBDown本地配置文件",
            ["opt_locale"] = "设置界面的语言环境 (zh, en, ru)",

            // Subcommands
            ["cmd_login"] = "通过APP扫描二维码以登录您的WEB账号",
            ["cmd_logintv"] = "通过APP扫描二维码以登录您的TV账号",
            ["cmd_serve"] = "以服务器模式运行",
            ["opt_listen"] = "服务器监听url",
            ["opt_max_concurrent"] = "最大并发下载数(默认3)",

            // General & Console Logs
            ["welcome_msg"] = "遇到问题请首先到以下地址查阅有无相关信息：",
            ["force_exit"] = "强制退出...",
            ["upgrade_retry"] = "请尝试升级到最新版本后重试!",
            ["date_format_err"] = "格式化日期出错: {0}",
            ["new_ver_found"] = "发现新版本：{0}",
            ["check_update_err"] = "检查更新失败: {0}",
            ["infinite_redirect"] = "无限重定向",
            ["no_page_info"] = "未找到任何分P信息",
            ["invalid_url_id"] = "输入有误：无法识别的视频 URL 或 ID",
            ["no_course_page"] = "未找到课程分P信息",
            ["no_bangumi_page"] = "未找到番剧分P信息",
            ["cookie_expired"] = "Cookie 已过期或无效 (code=-101)",
            ["login_check_err"] = "检测登录状态失败: {0}",
            ["ffmpeg_ DolbyVision_check_failed"] = "检测ffmpeg版本失败: {0}",
            ["mux_timeout_err"] = "{0} 混流操作超过 {1} 分钟未结束，已强制终止。请检查输入文件是否损坏或磁盘空间是否不足。",
            ["original_audio"] = "原音频",
            ["bg_audio_title"] = "背景音频",

            // Login Progress
            ["get_login_url"] = "获取登录地址...",
            ["gen_qrcode"] = "生成二维码...",
            ["gen_qrcode_success"] = "生成二维码成功: qrcode.png, 请打开并扫描, 或扫描打印的二维码",
            ["qrcode_expired"] = "二维码已过期, 请重新执行登录指令.",
            ["qrcode_scanned"] = "扫码成功, 请确认...",
            ["login_success"] = "登录成功! SESSDATA={0}",
            ["login_success_tv"] = "登录成功! AccessToken={0}",
            ["login_failed"] = "登录失败: {0}",

            // Downloader Logic & Workflow
            ["check_login"] = "检测账号登录...",
            ["cookie_expired_warn1"] = "  Cookie 已过期！",
            ["cookie_expired_warn2"] = "  请运行 BBDown login 重新扫码登录以获取新 Cookie。",
            ["cookie_expired_warn3"] = "  或者使用 --use-tv-api 配合 --access-token 下载。",
            ["not_logged_in_warn1"] = "  你尚未登录B站账号！",
            ["not_logged_in_warn2"] = "  未登录状态下仅能下载6分钟试看片段。",
            ["not_logged_in_warn3"] = "  请运行 BBDown login 扫码登录以获取完整视频。",
            ["get_aid"] = "获取aid...",
            ["get_aid_end"] = "获取aid结束: {0}",
            ["get_video_info"] = "获取视频信息...",
            ["ep_ss_not_found_bangumi"] = "未找到此 EP/SS 对应番剧信息, 正在尝试按课程查找。",
            ["new_aid"] = "新的 aid: {0}",
            ["get_video_info_err"] = "输入有误：无法获取视频信息",
            ["video_title"] = "视频标题: {0}",
            ["pub_time"] = "发布时间: {0}",
            ["video_url"] = "视频URL: {0}",
            ["up_space"] = "UP主页: {0}",
            ["interactive_video_warn"] = "视频为互动视频，暂时不支持tv下载，修改为默认下载",
            ["auto_selected_part"] = "程序已自动选择你输入的集数, 如果要下载其他集数请自行指定分P(如可使用-p ALL代表全部)",
            ["parse_part_param_failed"] = "解析分P参数时失败了~",
            ["pcdn_warn"] = "检测到{0}流为PCDN, 尝试强制替换为{1}……",
            ["intl_source_warn"] = "检测到视频流为外国源, 尝试强制替换为{0}……",
            ["video_stream"] = "视频",
            ["audio_stream"] = "音频",
            ["get_chapter_info_failed"] = "获取章节信息失败: {0}",
            ["replace_host_warn"] = "尝试将{0}流强制替换为{1}……",

            // Display & Track selection
            ["count_bg_audio"] = "共计{0}条背景音频流.",
            ["count_role_audio"] = "共计{0}条配音, 每条包含{1}条配音流.",
            ["count_video"] = "共计{0}条视频流.",
            ["count_audio"] = "共计{0}条音频流.",
            ["select_video_prompt"] = "请选择一条视频流(输入序号): ",
            ["select_audio_prompt"] = "请选择一条音频流(输入序号): ",
            ["merge_clips"] = "合并{0}分片...",
            ["clean_clips"] = "清理分片...",
            ["cmcc_multithread_disabled"] = "检测到cmcc域名cdn, 已经禁用多线程",

            // Download progress logs
            ["count_pages_selected"] = "共计 {0} 个分P, 已选择：{1}",
            ["pause_seconds"] = "停顿{0}秒...",
            ["parse_part_start"] = "开始解析P{0}: {1}... ({2} of {3})",
            ["aid_already_downloaded"] = "aid: {0}已下载过, 跳过下载...",
            ["task_completed"] = "任务完成",
            ["skip_ai_subtitle"] = "跳过下载AI字幕",
            ["downloading_subtitle"] = "下载字幕 {0} => {1}...",
            ["no_matching_video_stream"] = "没有找到符合要求的视频流",
            ["no_matching_audio_stream"] = "没有找到符合要求的音频流",
            ["downloading_danmaku_xml"] = "正在下载弹幕Xml文件",
            ["danmaku_xml_parse_failed"] = "弹幕Xml解析失败, 删除Xml...",
            ["danmaku_xml_empty"] = "当前视频没有弹幕, 删除Xml...",
            ["saving_danmaku_ass"] = "正在保存弹幕Ass文件...",
            ["selected_streams"] = "已选择的流:",
            ["file_exists_skipping"] = "{0}已存在, 跳过下载...",
            ["dovi_ffmpeg_warn"] = "检测到杜比视界清晰度且您的ffmpeg版本小于5.0,将使用mp4box混流...",
            ["downloading_video_part"] = "开始下载P{0}视频...",
            ["downloading_audio_part"] = "开始下载P{0}音频...",
            ["downloading_bg_audio_part"] = "开始下载P{0}背景配音...",
            ["downloading_dub_part"] = "开始下载P{0}配音[{1}]...",
            ["download_part_finished"] = "下载P{0}完毕",
            ["muxing_start"] = "开始合并音视频{0}...",
            ["and_subtitle"] = "和字幕",
            ["muxing_failed"] = "合并失败",
            ["clean_temp_files"] = "清理临时文件...",
            ["vip_needed_drm1"] = "此视频需要大会员登录才能获取完整DRM内容。",
            ["vip_needed_drm2"] = "请先运行: BBDown login  或使用 --cookie 参数",
            ["select_quality_prompt"] = "请选择最想要的清晰度(输入序号): ",
            ["count_flv_streams"] = "共计{0}条流(共有{1}个分段).",
            ["downloading_video_clip"] = "开始下载P{0}视频, 片段({1}/{2})...",
            ["merging_segments"] = "开始合并分段...",
            ["muxing_video_start"] = "开始混流视频{0}...",
            ["parse_part_failed"] = "解析此分P失败(建议--debug查看详细信息)",
            ["download_err_retry"] = "下载出现异常, 3秒后将进行自动重试..."
        },
        ["en"] = new(StringComparer.OrdinalIgnoreCase)
        {
            // CLI Options & Arguments
            ["opt_url"] = "Video URL or av|bv|BV|ep|ss",
            ["opt_use_tv_api"] = "Use TV API mode for parsing",
            ["opt_use_app_api"] = "Use APP API mode for parsing",
            ["opt_use_intl_api"] = "Use International API (Southeast Asia) mode for parsing",
            ["opt_use_mp4box"] = "Use MP4Box for mixing (muxing)",
            ["opt_encoding_priority"] = "Select priority for video/audio encoding, comma-separated (e.g. 'hevc,av1,avc,flac,eac3,m4a')",
            ["opt_dfn_priority"] = "Quality priority, comma-separated (e.g. '8K, 1080P High Rate, HDR, Dolby Vision')",
            ["opt_only_show_info"] = "Only show parsed video info without downloading",
            ["opt_show_all"] = "Show all page/part titles",
            ["opt_use_aria2c"] = "Use aria2c for downloading (need to prepare binary yourself)",
            ["opt_interactive"] = "Select quality interactively",
            ["opt_hide_streams"] = "Do not display available audio/video streams",
            ["opt_multi_thread"] = "Use multi-threaded download (enabled by default)",
            ["opt_simply_mux"] = "Simple muxing, do not add metadata description, author, etc.",
            ["opt_video_only"] = "Download video only",
            ["opt_audio_only"] = "Download audio only",
            ["opt_danmaku_only"] = "Download danmaku (comments) only",
            ["opt_cover_only"] = "Download cover image only",
            ["opt_sub_only"] = "Download subtitle only",
            ["opt_debug"] = "Output debug logs",
            ["opt_skip_mux"] = "Skip muxing step",
            ["opt_insecure"] = "Skip SSL certificate verification (only for proxy/capture scenarios)",
            ["opt_decrypt_drm"] = "Try to decrypt DRM-protected video",
            ["opt_drm_key"] = "DRM decryption key (hex)",
            ["opt_drm_kid"] = "DRM Key ID (hex)",
            ["opt_mp4decrypt_path"] = "Set the path to mp4decrypt",
            ["opt_wvd_path"] = "Set the path to device.wvd",
            ["opt_skip_subtitle"] = "Skip downloading subtitles",
            ["opt_skip_cover"] = "Skip downloading cover image",
            ["opt_force_http"] = "Force HTTP instead of HTTPS when downloading audio/video (enabled by default)",
            ["opt_download_danmaku"] = "Download danmaku",
            ["opt_download_danmaku_formats"] = "Specify danmaku formats to download, comma-separated",
            ["opt_skip_ai"] = "Skip downloading AI subtitles (enabled by default)",
            ["opt_video_ascending"] = "Sort video streams in ascending order (smallest size first)",
            ["opt_audio_ascending"] = "Sort audio streams in ascending order (smallest size first)",
            ["opt_allow_pcdn"] = "Do not replace PCDN domain, only used if normal and --upos-host fail",
            ["opt_file_pattern"] = "Customize single-part save filename using built-in variables",
            ["opt_multi_file_pattern"] = "Customize multi-part save filename using built-in variables",
            ["opt_select_page"] = "Select specific pages/parts: (-p 8 or -p 1,2 or -p 3-5 or -p ALL or -p LAST or -p 3,5,LATEST)",
            ["opt_language"] = "Set the mixed audio language code (e.g. chi, jpn)",
            ["opt_user_agent"] = "Specify user-agent, otherwise a random one is used",
            ["opt_cookie"] = "Set cookie string to download member-only content",
            ["opt_access_token"] = "Set access_token to download member-only TV/APP content",
            ["opt_aria2c_args"] = "Additional arguments passed to aria2c",
            ["opt_work_dir"] = "Set program working directory",
            ["opt_ffmpeg_path"] = "Set the path to ffmpeg",
            ["opt_mp4box_path"] = "Set the path to mp4box",
            ["opt_aria2c_path"] = "Set the path to aria2c",
            ["opt_upos_host"] = "Customize upos server host",
            ["opt_force_replace_host"] = "Force replacement of download host (enabled by default)",
            ["opt_save_archives"] = "Record downloaded videos to a local file to skip them in future runs",
            ["opt_delay_per_page"] = "Set download delay between parts in seconds (default: 0, no delay)",
            ["opt_muxer_timeout"] = "Muxing timeout in minutes (default: 30)",
            ["opt_retry_count"] = "Number of retries after network request failure (default: 3)",
            ["opt_retry_delay"] = "Base retry delay in milliseconds (default: 3000)",
            ["opt_thread_segment"] = "Segment size for multi-threaded download in MB (default: 20)",
            ["opt_host"] = "Specify BiliPlus host",
            ["opt_ep_host"] = "Specify BiliPlus EP host",
            ["opt_tv_host"] = "Customize TV request host",
            ["opt_area"] = "(hk|tw|th) BiliPlus area (required when using BiliPlus)",
            ["opt_config_file"] = "Read specified local BBDown configuration file",
            ["opt_locale"] = "Set localization language (zh, en, ru)",

            // Subcommands
            ["cmd_login"] = "Log in to your WEB account by scanning the QR code via Bilibili APP",
            ["cmd_logintv"] = "Log in to your TV account by scanning the QR code via Bilibili APP",
            ["cmd_serve"] = "Run in API server mode",
            ["opt_listen"] = "API Server listening URL",
            ["opt_max_concurrent"] = "Maximum concurrent downloads (default: 3)",

            // General & Console Logs
            ["welcome_msg"] = "If you encounter issues, please check for info at:",
            ["force_exit"] = "Force Exit...",
            ["upgrade_retry"] = "Please try upgrading to the latest version and try again!",
            ["date_format_err"] = "Error formatting date: {0}",
            ["new_ver_found"] = "Found new version: {0}",
            ["check_update_err"] = "Failed to check update: {0}",
            ["infinite_redirect"] = "Infinite redirect detected",
            ["no_page_info"] = "No part information found",
            ["invalid_url_id"] = "Invalid input: Unrecognized video URL or ID",
            ["no_course_page"] = "No course part information found",
            ["no_bangumi_page"] = "No bangumi part information found",
            ["cookie_expired"] = "Cookie expired or invalid (code=-101)",
            ["login_check_err"] = "Failed to detect login status: {0}",
            ["ffmpeg_ DolbyVision_check_failed"] = "Failed to check ffmpeg version: {0}",
            ["mux_timeout_err"] = "{0} muxing exceeded {1} minutes and was forced to terminate. Please check if the input file is corrupted or if disk space is insufficient.",
            ["original_audio"] = "Original Audio",
            ["bg_audio_title"] = "Background Audio",

            // Login Progress
            ["get_login_url"] = "Retrieving login URL...",
            ["gen_qrcode"] = "Generating QR code...",
            ["gen_qrcode_success"] = "QR Code generated successfully: qrcode.png, please scan it in your APP",
            ["qrcode_expired"] = "QR Code expired. Please re-run the login command.",
            ["qrcode_scanned"] = "QR Code scanned! Please confirm login on your phone.",
            ["login_success"] = "Login successful! SESSDATA={0}",
            ["login_success_tv"] = "Login successful! AccessToken={0}",
            ["login_failed"] = "Login failed: {0}",

            // Downloader Logic & Workflow
            ["check_login"] = "Checking account login status...",
            ["cookie_expired_warn1"] = "  Cookie expired!",
            ["cookie_expired_warn2"] = "  Please run 'BBDown login' to scan QR code again and get a new Cookie.",
            ["cookie_expired_warn3"] = "  Or use '--use-tv-api' with '--access-token' to download.",
            ["not_logged_in_warn1"] = "  You are not logged in!",
            ["not_logged_in_warn2"] = "  In guest status, you can only download the first 6 minutes of the video.",
            ["not_logged_in_warn3"] = "  Please run 'BBDown login' to log in and get the full video.",
            ["get_aid"] = "Fetching aid...",
            ["get_aid_end"] = "Fetching aid finished: {0}",
            ["get_video_info"] = "Fetching video info...",
            ["ep_ss_not_found_bangumi"] = "No anime/show info found for this EP/SS, trying to search in courses...",
            ["new_aid"] = "New aid: {0}",
            ["get_video_info_err"] = "Invalid input: Failed to get video information",
            ["video_title"] = "Video Title: {0}",
            ["pub_time"] = "Publish Time: {0}",
            ["video_url"] = "Video URL: {0}",
            ["up_space"] = "Uploader Space: {0}",
            ["interactive_video_warn"] = "Interactive video detected. TV download is not supported. Switched to default download.",
            ["auto_selected_part"] = "The program has automatically selected the parts you entered. To download other parts, please specify them manually (e.g. use '-p ALL' to download all parts).",
            ["parse_part_param_failed"] = "Failed to parse part parameter!",
            ["pcdn_warn"] = "Detected PCDN for {0} stream. Attempting to force replace with {1}...",
            ["intl_source_warn"] = "Detected international source for video stream. Attempting to force replace with {0}...",
            ["video_stream"] = "video",
            ["audio_stream"] = "audio",
            ["get_chapter_info_failed"] = "Failed to get chapter info: {0}",
            ["replace_host_warn"] = "Attempting to force replace {0} stream with {1}...",

            // Display & Track selection
            ["count_bg_audio"] = "Total of {0} background audio tracks.",
            ["count_role_audio"] = "Total of {0} dubbings, each containing {1} audio streams.",
            ["count_video"] = "Total of {0} video streams.",
            ["count_audio"] = "Total of {0} audio streams.",
            ["select_video_prompt"] = "Please select a video stream (enter index): ",
            ["select_audio_prompt"] = "Please select an audio stream (enter index): ",
            ["merge_clips"] = "Merging {0} clips...",
            ["clean_clips"] = "Cleaning up clips...",
            ["cmcc_multithread_disabled"] = "Detected CMCC CDN domain, multi-threading has been disabled",

            // Download progress logs
            ["count_pages_selected"] = "Total {0} parts, selected: {1}",
            ["pause_seconds"] = "Pausing for {0} seconds...",
            ["parse_part_start"] = "Start parsing P{0}: {1}... ({2} of {3})",
            ["aid_already_downloaded"] = "aid: {0} already downloaded, skipping...",
            ["task_completed"] = "Task completed",
            ["skip_ai_subtitle"] = "Skip downloading AI subtitles",
            ["downloading_subtitle"] = "Downloading subtitle {0} => {1}...",
            ["no_matching_video_stream"] = "No matching video stream found",
            ["no_matching_audio_stream"] = "No matching audio stream found",
            ["downloading_danmaku_xml"] = "Downloading danmaku XML file...",
            ["danmaku_xml_parse_failed"] = "Failed to parse danmaku XML, deleting XML...",
            ["danmaku_xml_empty"] = "No danmaku found for this video, deleting XML...",
            ["saving_danmaku_ass"] = "Saving danmaku ASS file...",
            ["selected_streams"] = "Selected streams:",
            ["file_exists_skipping"] = "{0} already exists, skipping...",
            ["dovi_ffmpeg_warn"] = "Detected Dolby Vision and ffmpeg version < 5.0, will use mp4box for muxing...",
            ["downloading_video_part"] = "Starting download of P{0} video...",
            ["downloading_audio_part"] = "Starting download of P{0} audio...",
            ["downloading_bg_audio_part"] = "Starting download of P{0} background audio...",
            ["downloading_dub_part"] = "Starting download of P{0} dubbing [{1}]...",
            ["download_part_finished"] = "Download of P{0} finished",
            ["muxing_start"] = "Starting to mix video and audio{0}...",
            ["and_subtitle"] = " and subtitles",
            ["muxing_failed"] = "Mixing failed",
            ["clean_temp_files"] = "Cleaning up temporary files...",
            ["vip_needed_drm1"] = "This video requires a VIP login to get full DRM content.",
            ["vip_needed_drm2"] = "Please run 'BBDown login' or use the '--cookie' parameter first.",
            ["select_quality_prompt"] = "Please select preferred quality (enter index): ",
            ["count_flv_streams"] = "Total of {0} streams (containing {1} segments).",
            ["downloading_video_clip"] = "Starting download of P{0} video, segment ({1}/{2})...",
            ["merging_segments"] = "Merging segments...",
            ["muxing_video_start"] = "Starting to mux video{0}...",
            ["parse_part_failed"] = "Failed to parse this part (suggest using --debug for details)",
            ["download_err_retry"] = "Download error. Auto-retrying in 3 seconds..."
        },
        ["ru"] = new(StringComparer.OrdinalIgnoreCase)
        {
            // CLI Options & Arguments
            ["opt_url"] = "Ссылка на видео или ID (av|bv|BV|ep|ss)",
            ["opt_use_tv_api"] = "Использовать TV API для парсинга",
            ["opt_use_app_api"] = "Использовать APP API для парсинга",
            ["opt_use_intl_api"] = "Использовать международный API (Юго-Восточная Азия)",
            ["opt_use_mp4box"] = "Использовать MP4Box для микширования",
            ["opt_encoding_priority"] = "Приоритет кодеков видео/аудио, через запятую (напр. 'hevc,av1,avc,flac,eac3,m4a')",
            ["opt_dfn_priority"] = "Приоритет качества, через запятую (напр. '8K, 1080P High Rate, HDR, Dolby Vision')",
            ["opt_only_show_info"] = "Показать только информацию о видео без скачивания",
            ["opt_show_all"] = "Показать заголовки всех серий/частей",
            ["opt_use_aria2c"] = "Использовать aria2c для скачивания (нужно подготовить исполняемый файл)",
            ["opt_interactive"] = "Интерактивный выбор качества",
            ["opt_hide_streams"] = "Не выводить список доступных видео- и аудиопотоков",
            ["opt_multi_thread"] = "Многопоточное скачивание (включено по умолчанию)",
            ["opt_simply_mux"] = "Упрощенное микширование (без добавления описания, автора и т.д.)",
            ["opt_video_only"] = "Скачать только видео",
            ["opt_audio_only"] = "Скачать только аудио",
            ["opt_danmaku_only"] = "Скачать только даньмаку (комментарии)",
            ["opt_cover_only"] = "Скачать только обложку",
            ["opt_sub_only"] = "Скачать только субтитры",
            ["opt_debug"] = "Выводить отладочные логи",
            ["opt_skip_mux"] = "Пропустить шаг микширования",
            ["opt_insecure"] = "Пропустить проверку SSL-сертификатов (только для прокси/перехвата трафика)",
            ["opt_decrypt_drm"] = "Попытаться расшифровать видео с DRM-защитой",
            ["opt_drm_key"] = "DRM ключ расшифровки (hex)",
            ["opt_drm_kid"] = "DRM Key ID (hex)",
            ["opt_mp4decrypt_path"] = "Путь к mp4decrypt",
            ["opt_wvd_path"] = "Путь к device.wvd",
            ["opt_skip_subtitle"] = "Пропустить скачивание субтитров",
            ["opt_skip_cover"] = "Пропустить скачивание обложки",
            ["opt_force_http"] = "Принудительно использовать HTTP вместо HTTPS при скачивании (включено по умолчанию)",
            ["opt_download_danmaku"] = "Скачать даньмаку",
            ["opt_download_danmaku_formats"] = "Форматы даньмаку для скачивания, через запятую",
            ["opt_skip_ai"] = "Пропустить скачивание ИИ-субтитров (включено по умолчанию)",
            ["opt_video_ascending"] = "Сортировка видео по возрастанию (сначала меньший объем)",
            ["opt_audio_ascending"] = "Сортировка аудио по возрастанию (сначала меньший объем)",
            ["opt_allow_pcdn"] = "Не заменять домен PCDN, использовать только при сбое обычного и --upos-host",
            ["opt_file_pattern"] = "Шаблон имени для сохранения одиночного видео",
            ["opt_multi_file_pattern"] = "Шаблон имени для сохранения многосерийного видео",
            ["opt_select_page"] = "Выбрать конкретные серии/части: (-p 8 или -p 1,2 или -p 3-5 или -p ALL или -p LAST или -p 3,5,LATEST)",
            ["opt_language"] = "Установить языковой код для микшируемой аудиодорожки (напр., chi, jpn)",
            ["opt_user_agent"] = "Указать User-Agent (иначе будет сгенерирован случайный)",
            ["opt_cookie"] = "Строка Cookie для скачивания приватного контента",
            ["opt_access_token"] = "Токен доступа access_token для контента TV/APP API",
            ["opt_aria2c_args"] = "Дополнительные аргументы для aria2c",
            ["opt_work_dir"] = "Рабочий каталог программы",
            ["opt_ffmpeg_path"] = "Путь к ffmpeg",
            ["opt_mp4box_path"] = "Путь к mp4box",
            ["opt_aria2c_path"] = "Путь к aria2c",
            ["opt_upos_host"] = "Пользовательский сервер Upos",
            ["opt_force_replace_host"] = "Принудительно заменять хост загрузки (включено по умолчанию)",
            ["opt_save_archives"] = "Сохранять историю скачиваний в локальный файл, чтобы пропускать их в будущем",
            ["opt_delay_per_page"] = "Задержка между загрузками серий в секундах (по умолчанию: 0, без задержки)",
            ["opt_muxer_timeout"] = "Таймаут микширования в минутах (по умолчанию: 30)",
            ["opt_retry_count"] = "Количество попыток при сбое сетевого запроса (по умолчанию: 3)",
            ["opt_retry_delay"] = "Базовая задержка повторной попытки в миллисекундах (по умолчанию: 3000)",
            ["opt_thread_segment"] = "Размер сегмента при многопоточном скачивании в МБ (по умолчанию: 20)",
            ["opt_host"] = "Указать хост BiliPlus",
            ["opt_ep_host"] = "Указать EP-хост BiliPlus",
            ["opt_tv_host"] = "Пользовательский хост запросов TV",
            ["opt_area"] = "(hk|tw|th) Регион BiliPlus (обязателен при использовании BiliPlus)",
            ["opt_config_file"] = "Использовать указанный локальный файл конфигурации BBDown",
            ["opt_locale"] = "Установить языковые настройки интерфейса (zh, en, ru)",

            // Subcommands
            ["cmd_login"] = "WEB авторизация через сканирование QR-кода в приложении Bilibili",
            ["cmd_logintv"] = "TV авторизация через сканирование QR-кода в приложении Bilibili",
            ["cmd_serve"] = "Запуск в режиме API-сервера",
            ["opt_listen"] = "Адрес и порт прослушивания сервера",
            ["opt_max_concurrent"] = "Максимальное количество одновременных загрузок (по умолчанию 3)",

            // General & Console Logs
            ["welcome_msg"] = "При возникновении проблем в первую очередь проверьте информацию по адресу:",
            ["force_exit"] = "Принудительный выход...",
            ["upgrade_retry"] = "Пожалуйста, попробуйте обновиться до последней версии и повторите попытку!",
            ["date_format_err"] = "Ошибка форматирования даты: {0}",
            ["new_ver_found"] = "Доступна новая версия: {0}",
            ["check_update_err"] = "Не удалось проверить наличие обновлений: {0}",
            ["infinite_redirect"] = "Обнаружено бесконечное перенаправление",
            ["no_page_info"] = "Информация о разделах/сериях не найдена",
            ["invalid_url_id"] = "Некорректный ввод: нераспознанный URL-адрес или идентификатор видео",
            ["no_course_page"] = "Информация о сериях курса не найдена",
            ["no_bangumi_page"] = "Информация о сериях аниме/шоу не найдена",
            ["cookie_expired"] = "Срок действия Cookie истек или недействителен (code=-101)",
            ["login_check_err"] = "Не удалось проверить статус авторизации: {0}",
            ["ffmpeg_ DolbyVision_check_failed"] = "Не удалось проверить версию ffmpeg: {0}",
            ["mux_timeout_err"] = "Операция микширования {0} превысила {1} минут и была принудительно остановлена. Убедитесь, что исходный файл не поврежден и достаточно свободного места на диске.",
            ["original_audio"] = "Оригинальное аудио",
            ["bg_audio_title"] = "Фоновое аудио",

            // Login Progress
            ["get_login_url"] = "Получение ссылки для входа...",
            ["gen_qrcode"] = "Генерация QR-кода...",
            ["gen_qrcode_success"] = "QR-код успешно сгенерирован: qrcode.png, пожалуйста, отсканируйте его в приложении",
            ["qrcode_expired"] = "Срок действия QR-кода истек. Пожалуйста, запустите команду авторизации заново.",
            ["qrcode_scanned"] = "QR-код отсканирован! Пожалуйста, подтвердите вход на телефоне.",
            ["login_success"] = "Вход выполнен успешно! SESSDATA={0}",
            ["login_success_tv"] = "Вход выполнен успешно! AccessToken={0}",
            ["login_failed"] = "Ошибка авторизации: {0}",

            // Downloader Logic & Workflow
            ["check_login"] = "Проверка авторизации...",
            ["cookie_expired_warn1"] = "  Срок действия Cookie истек!",
            ["cookie_expired_warn2"] = "  Пожалуйста, запустите 'BBDown login' для сканирования QR-кода заново.",
            ["cookie_expired_warn3"] = "  Или используйте '--use-tv-api' совместно с '--access-token'.",
            ["not_logged_in_warn1"] = "  Вы не авторизованы!",
            ["not_logged_in_warn2"] = "  Без авторизации доступно скачивание только первых 6 минут видео.",
            ["not_logged_in_warn3"] = "  Пожалуйста, запустите 'BBDown login' для авторизации и скачивания полного видео.",
            ["get_aid"] = "Получение aid...",
            ["get_aid_end"] = "Получение aid завершено: {0}",
            ["get_video_info"] = "Получение информации о видео...",
            ["ep_ss_not_found_bangumi"] = "Информация об аниме/шоу для этого EP/SS не найдена, пробуем найти в курсах...",
            ["new_aid"] = "Новый aid: {0}",
            ["get_video_info_err"] = "Некорректный ввод: не удалось получить информацию о видео",
            ["video_title"] = "Название видео: {0}",
            ["pub_time"] = "Дата публикации: {0}",
            ["video_url"] = "Ссылка на видео: {0}",
            ["up_space"] = "Страница автора: {0}",
            ["interactive_video_warn"] = "Обнаружено интерактивное видео. Загрузка через TV API не поддерживается, переключение на стандартный режим.",
            ["auto_selected_part"] = "Программа автоматически выбрала указанные серии. Для скачивания других серий укажите их вручную (например, '-p ALL' для всех серий).",
            ["parse_part_param_failed"] = "Не удалось распознать параметр выбора серий!",
            ["pcdn_warn"] = "Обнаружен PCDN для {0}-потока. Попытка принудительно заменить на {1}...",
            ["intl_source_warn"] = "Обнаружен международный источник для видеопотока. Попытка принудительно заменить на {0}...",
            ["video_stream"] = "видео",
            ["audio_stream"] = "аудио",
            ["get_chapter_info_failed"] = "Не удалось получить информацию о главах: {0}",
            ["replace_host_warn"] = "Попытка принудительно заменить {0}-поток на {1}……",

            // Display & Track selection
            ["count_bg_audio"] = "Всего {0} фоновых аудиодорожек.",
            ["count_role_audio"] = "Всего {0} озвучек, каждая содержит {1} аудиопотоков.",
            ["count_video"] = "Всего {0} видеопотоков.",
            ["count_audio"] = "Всего {0} аудиопотоков.",
            ["select_video_prompt"] = "Пожалуйста, выберите видеопоток (введите индекс): ",
            ["select_audio_prompt"] = "Пожалуйста, выберите аудиопоток (введите индекс): ",
            ["merge_clips"] = "Объединение сегментов {0}...",
            ["clean_clips"] = "Очистка временных файлов...",
            ["cmcc_multithread_disabled"] = "Обнаружен CDN-хост CMCC, многопоточность отключена",

            // Download progress logs
            ["count_pages_selected"] = "Всего {0} серий, выбрано: {1}",
            ["pause_seconds"] = "Пауза {0} сек...",
            ["parse_part_start"] = "Начало парсинга P{0}: {1}... ({2} из {3})",
            ["aid_already_downloaded"] = "aid: {0} уже скачан, пропуск...",
            ["task_completed"] = "Задача выполнена",
            ["skip_ai_subtitle"] = "Пропуск скачивания ИИ-субтитров",
            ["downloading_subtitle"] = "Скачивание субтитров {0} => {1}...",
            ["no_matching_video_stream"] = "Подходящий видеопоток не найден",
            ["no_matching_audio_stream"] = "Подходящий аудиопоток не найден",
            ["downloading_danmaku_xml"] = "Скачивание XML даньмаку...",
            ["danmaku_xml_parse_failed"] = "Ошибка парсинга XML даньмаку, удаление XML...",
            ["danmaku_xml_empty"] = "У этого видео нет даньмаку, удаление XML...",
            ["saving_danmaku_ass"] = "Сохранение ASS даньмаку...",
            ["selected_streams"] = "Выбранные потоки:",
            ["file_exists_skipping"] = "{0} уже существует, пропуск загрузки...",
            ["dovi_ffmpeg_warn"] = "Обнаружен Dolby Vision и версия ffmpeg < 5.0, для микширования будет использован mp4box...",
            ["downloading_video_part"] = "Начало загрузки видео P{0}...",
            ["downloading_audio_part"] = "Начало загрузки аудио P{0}...",
            ["downloading_bg_audio_part"] = "Начало загрузки фонового аудио P{0}...",
            ["downloading_dub_part"] = "Начало загрузки озвучки P{0} [{1}]...",
            ["download_part_finished"] = "Загрузка P{0} завершена",
            ["muxing_start"] = "Начало микширования видео и аудио{0}...",
            ["and_subtitle"] = " и субтитров",
            ["muxing_failed"] = "Сбой объединения",
            ["clean_temp_files"] = "Очистка временных файлов...",
            ["vip_needed_drm1"] = "Это видео требует VIP-авторизации для доступа к DRM-контенту.",
            ["vip_needed_drm2"] = "Пожалуйста, запустите 'BBDown login' или используйте параметр '--cookie'.",
            ["select_quality_prompt"] = "Пожалуйста, выберите качество (введите индекс): ",
            ["count_flv_streams"] = "Всего {0} потоков (содержит {1} сегментов).",
            ["downloading_video_clip"] = "Начало загрузки видео P{0}, сегмент ({1}/{2})...",
            ["merging_segments"] = "Объединение сегментов...",
            ["muxing_video_start"] = "Начало микширования видео{0}...",
            ["parse_part_failed"] = "Не удалось разобрать эту серию (используйте --debug для деталей)",
            ["download_err_retry"] = "Ошибка загрузки. Автоматический повтор через 3 секунды..."
        }
    };

    public static string GetString(string key)
    {
        var culture = CultureName;
        if (Translations.TryGetValue(culture, out var dict) && dict.TryGetValue(key, out var val))
        {
            return val;
        }

        // Fallback to "en"
        if (Translations["en"].TryGetValue(key, out val))
        {
            return val;
        }

        // Fallback to "zh"
        if (Translations["zh"].TryGetValue(key, out val))
        {
            return val;
        }

        return key;
    }

    public static string GetString(string key, params object?[] args)
    {
        var format = GetString(key);
        try
        {
            return string.Format(format, args);
        }
        catch
        {
            return format;
        }
    }
}
