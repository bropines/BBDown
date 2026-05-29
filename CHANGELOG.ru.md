[English](CHANGELOG.en.md) | [Русский](CHANGELOG.ru.md) | [简体中文](CHANGELOG.md)

# История изменений (Changelog)

Все значимые изменения в этом проекте документируются в данном файле.
Этот файл ведется в соответствии с правилами [Keep a Changelog](https://keepachangelog.com/ru/1.1.0/), а версии соответствуют спецификации [Semantic Versioning](https://semver.org/).

## [1.6.4] - 2026-05-29

### Добавлено

- **Дешифрование Widevine DRM на чистом C#** (полностью заменяет внешнюю зависимость от Python/pywidevine)
  - Реализована деривация ключей контекста `WidevineCrypto.AesCmac` + `derive_keys` / `derive_context`
  - Полная подпись HMAC-SHA256 и расшифровка контента AES
  - Поддержка формата WVD V2 + совместимость с открытым ключом PKCS#1 сертификата сервера Bilibili
- Автоматизированный пайплайн GitHub Release (пуш тега `v*` автоматически собирает бинарники под 6 платформ и создает релиз на GitHub)
- Настройка количества параллельных загрузок в API сервере: `BBDown serve --max-concurrent <n>`
- Дополнительные настройки в CLI:
  - `--muxer-timeout <минуты>` — лимит времени микширования (по умолчанию 30)
  - `--retry-count <n>` — количество повторных попыток сетевых запросов (по умолчанию 3)
  - `--retry-delay <миллисекунды>` — базовая задержка перед повтором (по умолчанию 3000)
  - `--thread-segment-size <МБ>` — размер сегмента многопоточной загрузки (по умолчанию 20)
- Обнаружение истекших Cookie с понятным выводом логов ("Требуется авторизация" / "Cookie истек")
- Проброс `CancellationToken` во все цепочки скачивания (отмена по Ctrl+C в CLI и отмена запросов в API)
- Докачка временных файлов `.tmp` (автоматическое возобновление скачанных сегментов после сбоя)
- Логирование API сервера в файл (`bbdown-api.log`)
- Методы безопасного считывания JSON-объектов `JsonElementExtensions` (10 методов расширения)
- Каркас модульных тестов: `BBDown.Tests` (`BilibiliBvConverterTests` / `UrlResolverTests` / `FormatHelperTests`)
- Разделение утилит на `UrlResolver.cs` и `ExternalToolHelper.cs`

### Изменено

- **Обновление целевой платформы .NET: .NET 9 → .NET 10**
- Обновление зависимостей: QRCoder 1.6.0 → 1.8.0
- Обновление зависимостей: Google.Protobuf 3.28.3 → 3.34.1
- Обновление зависимостей: Grpc.Tools 2.67.0 → 2.80.0
- Смена библиотеки CLI: устаревшая System.CommandLine → Spectre.Console.Cli 0.55.0
- Рефакторинг глобального состояния `Config`: структура `AppSettings` + потокобезопасная блокировка чтения/записи
- Обновление пула соединений `HttpClient`: `SocketsHttpHandler.PooledConnectionLifetime = 5min`
- Стандартизация файла документации API: `json-api-doc.md` → `API.md`
- Детализированная политика повторов: экспоненциальная задержка + пропуск при невосстановимых ошибках (`ArgumentException` / `InvalidOperationException` / `NotSupportedException`)
- Очистка лишних ссылок NuGet: `Microsoft.Extensions.DependencyInjection` (неявно поставляется в `Microsoft.NET.Sdk.Web`)

### Исправлено

- **Захват портов при запуске API сервера через `dotnet run`**: удален `launchSettings.json`, параметр `serve --listen` теперь корректно биндится на указанные адреса
- **Соответствие структуры Widevine Protobuf стандартам Google**: исправлена нумерация полей (`pssh_data=1`, перечисления `RequestType`, тип `key_control_nonce=uint32`)
- **Сбои Native AOT в рантайме**: добавлены аннотации `[DynamicallyAccessedMembers]` классам `MyOption` / `CommandSettings` / `Command` и настройка `<TrimmerRootAssembly Include="BBDown" />`
- Скрытие консольных окон FFmpeg/MP4Box на Windows при микшировании (`CreateNoWindow = true`)
- Кроссплатформенное создание директорий (использование `Path.GetDirectoryName` вместо проверок `/`)
- Восстановлены потерянные сообщения об ошибках при сетевых повторах скачивания (добавлен `LogDebug`)
- Защита от необработанных исключений в колбэках вебхуков API сервера
- Нечисловые вводы в `Parser.GetMaxQn` заменены на безопасный `int.TryParse` (вместо `int.Parse`)
- Ошибка экранирования двойных кавычек в `BBDownMuxer.EscapeString`
- Безопасная обработка пустых коллекций в вызовах `First()`, предотвращающая `InvalidOperationException`
- Безопасная обработка нечисловых aid в геттере `Page.bvid`
- Защита от выхода за границы пустого массива в `MergeFLV`
- Объединены дублирующиеся хелперы имен файлов в `SpaceVideoFetcher` и `BBDownUtil` в общий `BBDown.Core.Util.PathUtil`
- Проверка на null при вызове `Path.GetDirectoryName`
- Добавлена валидация аргументов перед `Convert.ToInt64` в `AppHelper.DoReqAsync`
- Баги с турецким locale устранены переводом `ToLower()` на `ToLowerInvariant()`
- Исправлены утечки памяти ресурсов `JsonDocument` и `HttpResponseMessage`
- Предотвращено деление на ноль в колбэках прогресса `BBDownDownloadUtil`
- Устранены дедлоки при микшировании FFmpeg/MP4Box за счет корректного считывания потока stdout
- Устранена коллизия файлов при параллельном скачивании через блокировки путей (`SemaphoreSlim`)
- Скрыта подробная информация об ошибках API сервера (по умолчанию скрыта в `ErrorMessage`, видна только в режиме debug)

## [1.6.3] - 2025-05-06

### Исправлено

- Исправлена ошибка, из-за которой параметр `DelayPerPage` ошибочно требовался как обязательный в System.CommandLine beta4.

## [1.6.2] - 2025-03-16

### Исправлено

- Оптимизация процесса сборки Dockerfile.
- Исправлено несколько случаев неосвобожденных ресурсов `JsonDocument`.
- Повышена безопасность доступа к свойствам в `NormalInfoFetcher.TryGetProperty`.

## [1.6.1] - 2025-02-08

### Добавлено

- Поддержка формата даньмаку ASS.
- Поддержка нового формата ссылок на плейлисты (`space.bilibili.com/*/lists/*`).

### Исправлено

- Исправлен запрос HEAD в `GetWebLocationAsync`.

## [1.6.0] - 2024-12-15

### Добавлено

- Встроенное дешифрование Widevine DRM на C# (без Python).
- Режим API-сервера (`BBDown serve`).
- Поддержка файлов конфигурации (`BBDown.config`).

### Изменено

- Рефакторинг моделей запросов gRPC APP API.
- Поддержка нескольких аудиодорожек (фоновое аудио, озвучка и т.д.).

---

[unreleased]: https://github.com/AliverAnme/BBDown/compare/v1.6.3...HEAD
[1.6.3]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.3
[1.6.2]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.2
[1.6.1]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.1
[1.6.0]: https://github.com/AliverAnme/BBDown/releases/tag/v1.6.0
