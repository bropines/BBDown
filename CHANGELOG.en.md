[English](CHANGELOG.en.md) | [Русский](CHANGELOG.ru.md) | [简体中文](CHANGELOG.md)

# Changelog

All notable changes to this project will be documented in this file.
This file follows the [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) format, and versioning adheres to [Semantic Versioning](https://semver.org/).

## [1.6.4] - 2026-05-29

### Added

- **Native C# Widevine DRM Decryption** (completely replaces Python/pywidevine dependency)
  - Implemented `WidevineCrypto.AesCmac` + `derive_keys` / `derive_context` context key derivation
  - Full HMAC-SHA256 signature verification + AES content key decryption
  - Support for V2 WVD format + Bilibili server certificate PKCS#1 public key compatibility
- GitHub Release automation workflow (pushing `v*` tags automatically builds on 6 platforms and creates a Release)
- Customized concurrent downloads limit on API server: `BBDown serve --max-concurrent <n>`
- New CLI configuration options:
  - `--muxer-timeout <minutes>` — Muxing process timeout (default: 30)
  - `--retry-count <n>` — Number of network request retries (default: 3)
  - `--retry-delay <milliseconds>` — Base retry delay (default: 3000)
  - `--thread-segment-size <MB>` — Multi-threaded download segment size (default: 20)
- Cookie expiration detection and explicit logging prompts ("Not Logged In" vs "Cookie Expired")
- Full cancellation token implementation (`CancellationToken`) throughout download paths (CLI Ctrl+C / API requests cancellation)
- Support for resuming unfinished downloads using `.tmp` files (auto-move of completed temp files and write verification fixes)
- API Server file logging (`bbdown-api.log`)
- `JsonElementExtensions` safe JSON reader utilities (10 extension methods)
- Unit test framework setup: `BBDown.Tests` (`BilibiliBvConverterTests` / `UrlResolverTests` / `FormatHelperTests`)
- Core classes partitioned: `UrlResolver.cs` / `ExternalToolHelper.cs`

### Changed

- **Target framework upgraded: .NET 9 → .NET 10**
- Dependency upgrade: QRCoder 1.6.0 → 1.8.0
- Dependency upgrade: Google.Protobuf 3.28.3 → 3.34.1
- Dependency upgrade: Grpc.Tools 2.67.0 → 2.80.0
- Migrated CLI engine: System.CommandLine (archived) → Spectre.Console.Cli 0.55.0
- Refactored `Config` global state: `AppSettings` record + thread-safe read/write lock
- Connection pool refreshing for `HttpClient`: `SocketsHttpHandler.PooledConnectionLifetime = 5min`
- Standardized API documentation filename: `json-api-doc.md` → `API.md`
- Fine-grained retry strategy: exponential backoff + short-circuit on non-retryable exceptions (`ArgumentException` / `InvalidOperationException` / `NotSupportedException`)
- Cleaned up redundant NuGet package references: `Microsoft.Extensions.DependencyInjection` (implicitly provided by `Microsoft.NET.Sdk.Web`)

### Fixed

- **API server `dotnet run` port hijacking**: Removed `launchSettings.json`. `serve --listen` now correctly binds custom addresses
- **Widevine Protobuf compliance**: Aligned fields and layouts with Google standards (`pssh_data=1`, `RequestType` enums, `key_control_nonce=uint32`)
- **Native AOT runtime crashes**: Added `[DynamicallyAccessedMembers]` to `MyOption` / `CommandSettings` / `Command` classes and added `<TrimmerRootAssembly Include="BBDown" />`
- Muxing process console window pop-ups hidden on Windows (`CreateNoWindow = true` for FFmpeg/MP4Box)
- Multi-platform directory creation logic (`Path.GetDirectoryName` replaces `Contains('/')`)
- Restored missing exception messages when retrying downloads (added `LogDebug`)
- Safety check for unobserved exceptions in API server webhook callbacks
- Non-numeric inputs handling in `Parser.GetMaxQn` (replaced `int.Parse` with `int.TryParse`)
- Escape syntax bugs in `BBDownMuxer.EscapeString` for double quotes
- Safe handling of empty sequences in multiple `First()` calls to prevent `InvalidOperationException`
- Safe handling of non-numeric aid parameters in `Page.bvid` getter
- Array empty bounds safety in `MergeFLV`
- Merged duplicate filename helper implementations in `SpaceVideoFetcher` and `BBDownUtil` into `BBDown.Core.Util.PathUtil`
- Guard checks when `Path.GetDirectoryName` returns null
- Added missing validation for parameters passed directly to `Convert.ToInt64` in `AppHelper.DoReqAsync`
- Turkish locale character bugs resolved by replacing `ToLower()` with `ToLowerInvariant()`
- Disposed unused resources to fix multiple `JsonDocument` and `HttpResponseMessage` memory leaks
- Prevented potential divide-by-zero occurrences in `BBDownDownloadUtil` progress callbacks
- Solved deadlocks in FFmpeg/MP4Box muxing by properly consuming stdout streams
- Solved download target resource collision using path-based exclusivity locks (`SemaphoreSlim`)
- API server error details masked (hidden from `ErrorMessage` by default, exposed only in debug mode)

## [1.6.3] - 2025-05-06

### Fixed

- Fixed a bug where `DelayPerPage` was mistakenly required in System.CommandLine beta4.

## [1.6.2] - 2025-03-16

### Fixed

- Dockerfile build workflow improvements.
- Fixed multiple instances of undisposed `JsonDocument` resources.
- Safe property accesses in `NormalInfoFetcher.TryGetProperty`.

## [1.6.1] - 2025-02-08

### Added

- Support for ASS danmaku subtitle format.
- Compatibility with new playlist/collection URLs (`space.bilibili.com/*/lists/*`).

### Fixed

- Fixed HEAD request compatibility in `GetWebLocationAsync`.

## [1.6.0] - 2024-12-15

### Added

- Native C# Widevine DRM decryption (removes Python requirement).
- API Server mode (`BBDown serve`).
- Configuration file support (`BBDown.config`).

### Changed

- Refactored gRPC request schemas for APP endpoint.
- Added support for multiple audio tracks (background, dubbing, etc.).

---

[unreleased]: https://github.com/AliverAnme/BBDown/compare/v1.6.3...HEAD
[1.6.3]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.3
[1.6.2]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.2
[1.6.1]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.1
[1.6.0]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.0
