[English](README.en.md) | [Русский](README.ru.md) | [简体中文](README.md)

[![img](https://img.shields.io/github/stars/AliverAnme/BBDown?label=Stars)](https://github.com/AliverAnme/BBDown)  [![img](https://img.shields.io/github/last-commit/AliverAnme/BBDown?label=Last%20Commit)](https://github.com/AliverAnme/BBDown)  [![img](https://img.shields.io/github/release/AliverAnme/BBDown?label=Latest%20Version)](https://github.com/AliverAnme/BBDown/releases)  [![img](https://img.shields.io/github/license/AliverAnme/BBDown?label=License)](https://github.com/AliverAnme/BBDown)  [![Build Latest](https://github.com/AliverAnme/BBDown/actions/workflows/build_latest.yml/badge.svg)](https://github.com/AliverAnme/BBDown/actions/workflows/build_latest.yml)

> This project is for personal learning, research, and non-commercial purposes only. Users must ensure compliance with relevant laws and regulations when using this tool, especially copyright-related legal provisions. The developer is not responsible for any copyright disputes or legal liability arising from the use of this tool. Please use it with caution, ensure your actions are legal and compliant, and only use relevant content under legal authorization.

# BBDown
A command-line Bilibili video downloader.

# Note
This software requires external tools for muxing (mixing):

* Normal Video: [ffmpeg](https://www.gyan.dev/ffmpeg/builds/) or [mp4box](https://gpac.wp.imt.fr/downloads/)
* Dolby Vision: ffmpeg 5.0+ or a recent version of mp4box.

# Quick Start
This software is published as a [Dotnet Tool](https://www.nuget.org/packages/BBDown/).

If you have a local dotnet environment, you can install it using the following command:
```
dotnet tool install --global BBDown
```

To update BBDown:
```
dotnet tool update --global BBDown
```

# Download
Release Version: https://github.com/AliverAnme/BBDown/releases

Automatically Built Test Version: https://github.com/AliverAnme/BBDown/actions

# Get Started
Run `BBDown --help` to see the full list of available parameters and commands:

```bash
BBDown --help
```

Quick lookup of core options:

| Short | Long Option | Description |
|--------|--------|------|
| `-t` | `--use-tv-api` | Use TV API mode for parsing |
| `-a` | `--use-app-api` | Use APP API mode for parsing |
| `-I` | `--only-show-info` | Only show parsed video info without downloading |
| `-i` | `--interactive` | Select quality interactively |
| `-d` | `--download-danmaku` | Download danmaku (comments) |
| `-e` | `--encoding-priority` | Encoding priority (e.g., `hevc,av1,avc`) |
| `-q` | `--dfn-priority` | Quality priority |
| `-p` | `--select-page` | Select pages/parts (e.g., `-p 1,3,5-10`) |
| `-F` | `--file-pattern` | Customize single-part save filename pattern |
| `-M` | `--multi-file-pattern` | Customize multi-part save filename pattern |
| `-c` | `--cookie` | Set Cookie string |
| | `--muxer-timeout` | Muxing timeout in minutes (default: 30) |
| | `--retry-count` | Number of retries after network request failure (default: 3) |
| | `--retry-delay` | Base retry delay in milliseconds (default: 3000) |
| | `--thread-segment-size` | Segment size for multi-threaded download in MB (default: 20) |
| | `--config-file` | Specify path to configuration file |

Commands:
- `login` — Log in to your WEB account by scanning the QR code via Bilibili APP
- `logintv` — Log in to your TV account by scanning the QR code via Bilibili APP
- `serve` — Run in API server mode
  - `-l, --listen` — Server listening URL (default: `http://0.0.0.0:23333`)
  - `--max-concurrent` — Maximum concurrent downloads (default: 3)

# Features
- [x] Anime/Show downloads (Web|TV|App)
- [x] Course downloads (Web)
- [x] Normal video downloads (Web|TV|App)
- [x] Multi-part/collection/playlist/favorites/uploader-space parsing
- [x] Auto-downloading multiple parts
- [x] Specifying custom parts to download
- [x] Interactive quality selection
- [x] Downloading external subtitles and converting them to srt/ass formats
- [x] Auto-muxing audio + video + subtitles + **chapters** `(using ffmpeg or mp4box)`
- [x] Downloading video, audio, or subtitles separately
- [x] QR code login support
- [x] Multi-threaded download (custom segment size)
- [x] External download engine support via aria2c
- [x] AVC/HEVC/AV1 encoding support
- [x] **8K / HDR / Dolby Vision / Dolby Atmos download support**
- [x] **Widevine DRM Decryption (native C# implementation, no Python needed)**
- [x] Custom storage filename patterns
- [x] **API Server Mode** (`BBDown serve`, supports concurrency limits and file logs)
- [x] **Configuration File Support** (`BBDown.config`)
- [x] **Ctrl+C Download Cancellation** (fully propagated CancellationToken)
- [x] **.tmp Breakpoint Resume** (auto-detects downloaded temp files after crash)

# TODO

## Completed ✅

- [x] API server download task queue limits (`SemaphoreSlim(3)` concurrency control)
- [x] `HttpClient` DNS lifetime configuration (`SocketsHttpHandler.PooledConnectionLifetime = 5min`)
- [x] `BBDownMuxer.RunExe` timeout mechanism (30-minute ceiling + force-kill)
- [x] Path-based download target exclusivity locks (`SemaphoreSlim`)
- [x] Refinement of exception granularity (28 generic `Exception` blocks replaced with semantic types)
- [x] Refinement of retry strategy (exponential backoff + bypass on non-retryable exceptions)
- [x] Full `CancellationToken` flow in download pipeline (CLI Ctrl+C / API cancel requests)
- [x] Configuration structure state refactoring (`AppSettings` record + thread-safe locks)
- [x] `.tmp` breakpoint resumption support (auto-move of temp files, write check fixes)
- [x] API server logging to file (`bbdown-api.log`)
- [x] Safe JSON accessor helpers (`JsonElementExtensions` with 10 extension methods)
- [x] Unit test framework setup: `BBDown.Tests` (`BilibiliBvConverterTests` / `UrlResolverTests` / `FormatHelperTests`)
- [x] Partition of core methods: `UrlResolver.cs` / `ExternalToolHelper.cs`
- [x] Custom API concurrency limit (`--max-concurrent` option for API server)
- [x] Cookie expiration detection and explicit prompts ("Not Logged In" vs "Cookie Expired")
- [x] Universal JSON parsing error wrapper (safe accessors migrated for 200+ calls)

## Pending 🔴

_All high-priority TODOs are currently completed. Future enhancements include expanding unit test coverage, adding more CLI/HTTP customization parameters, etc._

# Usage Guide

<details>
<summary>Configuration File (NEW)</summary> 

---

In version `1.4.9` or later, BBDown supports reading a local configuration file to simplify command-line inputs.

If `--config-file` is not specified, it defaults to reading the `BBDown.config` file located in the program directory. If specified, it reads that particular file.

A typical configuration file structure:
```config
# This is a BBDown configuration file
# Lines starting with # are ignored by the program
# The program reads the remaining non-empty lines, options and arguments should be on separate lines

# For example, setting the output filename format:
--file-pattern
<videoTitle>[<dfn>]

--multi-file-pattern
<videoTitle>/[P<pageNumberWithZero>]<pageTitle>[<dfn>]

# Setting download delay between parts to 2 seconds
--delay-per-page
2

# Enabling danmaku download
--download-danmaku
```

</details>

<details>
<summary>Custom Output Filename Format (NEW)</summary> 

---

In version `1.4.9` or later, BBDown allows users to customize the output filename format during muxing.
| Placeholder | Description |
|  ----  | ----  |
| `<videoTitle>` | Main title of the video |
| `<pageNumber>` | Video part index |
| `<pageNumberWithZero>` | Video part index (padded with leading zeros) |
| `<pageTitle>` | Title of the specific part |
| `<bvid>` | Video BVID |
| `<aid>` | Video AID |
| `<cid>` | Video CID |
| `<dfn>` | Video stream quality description |
| `<res>` | Video stream resolution |
| `<fps>` | Video stream frame rate |
| `<videoCodecs>` | Video codec |
| `<videoBandwidth>` | Video stream bitrate |
| `<audioCodecs>` | Audio codec |
| `<audioBandwidth>` | Audio stream bitrate |
| `<ownerName>` | Uploading user name (empty for anime/shows) |
| `<ownerMid>` | Uploading user MID (empty for anime/shows) |
| `<publishDate>` | Publish date (format: yyyy-MM-dd_HH-mm-ss) |
| `<apiType>` | API endpoint type (TV/APP/INTL/WEB) |

</details>

<details>
<summary>WEB/TV Authentication</summary>  

---
  
Log in to your WEB account via QR code scan:
```
BBDown login
```
Follow the prompts on your screen.

Log in to your TV account via QR code scan:
```
BBDown logintv
```
Follow the prompts on your screen.
 
*PS: If you get a `The type initializer for 'Gdip' threw an exception` error, please check [#37](https://github.com/AliverAnme/BBDown/issues/37) for solutions.*

Loading WEB cookies manually:
```
BBDown -c "SESSDATA=******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
Loading TV access tokens manually:
```
BBDown -tv -token "******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```

</details>

<details>
<summary>APP Authentication</summary>  

---

> According to [#123](https://github.com/AliverAnme/BBDown/issues/123#issuecomment-877583825), the `access_token` generated by TV login can also be passed to APP interfaces. Copying `BBDownTV.data` to `BBDownApp.data` allows the program to read it automatically.

Currently, the program cannot automatically fetch APP authorization. It is recommended to use a **packet capture tool** to acquire it.

Search for the key `authorization` in the request headers. Its value should look like `identify_v1 5227************1`. The token (access_key) is the string `5227************1`.

Once acquired, you can pass it manually via the `-token` command or save it in `BBDownApp.data` to let the program read it automatically.
  
```
BBDown -app -token "******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```

</details>

<details>
<summary>Widevine DRM Decryption</summary>

---

BBDown now features a **native C#** implementation of a Widevine CDM. It automatically queries decryption keys and decrypts Bilibili's DRM-protected content **without requiring Python or pywidevine**.

**Setup**
1. Obtain a `device.wvd` file (Widevine device file; you must extract this yourself or get it from a trusted source).
2. Place `device.wvd` in any of the following directories:
   - The same directory as the executable
   - A folder in your environment's `PATH` variable
   - macOS: `/opt/homebrew/bin` / Linux: `/usr/local/bin` / Windows: Program folder

**Usage**
```shell
# Download DRM protected videos (automatically decrypted)
BBDown --decrypt-drm "https://www.bilibili.com/cheese/play/ep1243104"
```

You can specify a custom path to `device.wvd` using parameters or by putting it in your working directory.

**Mechanism**
- Requests standard Widevine streams using `drm_tech_type=2`.
- Queries keys from the license server (compatibility depends on the `security_level` of your `device.wvd` file).
- Decrypts and mixes/muxes the outputs normally.

</details>

<details>
<summary>Common Commands</summary>  

---

Download a standard video:
```
BBDown "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
Download using the TV API (highly recommended, as large uploaders generally have watermark-free video streams):
```
BBDown -tv "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
When a video has too many parts, the program collapses them. Use the following command to display all parts:
```
BBDown --show-all "https://www.bilibili.com/video/BV1At41167aj"
```
Three ways to download specific parts:
* Single part (e.g. part 10):
```
BBDown "https://www.bilibili.com/video/BV1At41167aj?p=10"
BBDown -p 10 "https://www.bilibili.com/video/BV1At41167aj"
```
* Multiple parts (e.g. parts 1, 2, and 10):
```
BBDown -p 1,2,10 "https://www.bilibili.com/video/BV1At41167aj"
```
* Range of parts (e.g. parts 1 through 10):
```
BBDown -p 1-10 "https://www.bilibili.com/video/BV1At41167aj"
```
Download all parts of an anime/show:
```
BBDown -p ALL "https://www.bilibili.com/bangumi/play/ss33073"
```

</details>

<details>
<summary>API Server</summary>

Start the server (custom listening address and port):

```shell
BBDown serve -l http://0.0.0.0:12450
```

The API server does not support HTTPS configurations natively. If needed, please set up a reverse proxy using nginx or a similar tool.

For details about the API endpoints, refer to [API.md](./API.md).
</details>

# Demo
![1](https://user-images.githubusercontent.com/20772925/88686407-a2001480-d129-11ea-8aac-97a0c71af115.gif)

Check the MP4 file in the current directory after downloading is finished:

![2](https://user-images.githubusercontent.com/20772925/88478901-5e1cdc00-cf7e-11ea-97c1-154b9226564e.png)

## Development and Building

```bash
# Clone the repository
git clone https://github.com/AliverAnme/BBDown.git
cd BBDown

# Restore dependencies and build
dotnet restore
dotnet build

# Run the app
BBDown/bin/Debug/net10.0/BBDown --help
```

For guidelines on how to contribute, see [CONTRIBUTING.md](./CONTRIBUTING.md).

## Changelog

See [CHANGELOG.md](./CHANGELOG.md) to inspect version history.

## License

This project is licensed under the [MIT License](./LICENSE).

## Security

Please report vulnerabilities following the instructions in [SECURITY.md](./SECURITY.md). Do not submit security issues via public issues.

## Community

- [Contribution Guide](./CONTRIBUTING.md)
- [Code of Conduct](./CODE_OF_CONDUCT.md)
- [Discussions](https://github.com/AliverAnme/BBDown/discussions)

# Acknowledgments

This project is derived from [nilaoda/BBDown](https://github.com/nilaoda/BBDown). We thank the original author for their groundbreaking work.

### Additional branch acknowledgments:
* https://github.com/spectreconsole/spectre.console

### Original author acknowledgments:
* https://github.com/codebude/QRCoder
* https://github.com/icsharpcode/SharpZipLib
* https://github.com/protocolbuffers/protobuf
* https://github.com/grpc/grpc
* https://github.com/SocialSisterYi/bilibili-API-collect
* https://github.com/SeeFlowerX/bilibili-grpc-api
* https://github.com/FFmpeg/FFmpeg
* https://github.com/gpac/gpac
* https://github.com/aria2/aria2
