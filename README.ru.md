[English](README.en.md) | [Русский](README.ru.md) | [简体中文](README.md)

[![img](https://img.shields.io/github/stars/AliverAnme/BBDown?label=Stars)](https://github.com/AliverAnme/BBDown)  [![img](https://img.shields.io/github/last-commit/AliverAnme/BBDown?label=%D0%9F%D0%BE%D1%81%D0%BB%D0%B5%D0%B4%D0%BD%D0%B8%D0%B9%20%D0%BA%D0%BE%D0%BC%D0%BC%D0%B8%D1%82)](https://github.com/AliverAnme/BBDown)  [![img](https://img.shields.io/github/release/AliverAnme/BBDown?label=%D0%92%D0%B5%D1%80%D1%81%D0%B8%D1%8F)](https://github.com/AliverAnme/BBDown/releases)  [![img](https://img.shields.io/github/license/AliverAnme/BBDown?label=%D0%9B%D0%B8%D1%86%D0%B5%D0%BD%D0%B7%D0%B8%D1%8F)](https://github.com/AliverAnme/BBDown)  [![Build Latest](https://github.com/AliverAnme/BBDown/actions/workflows/build_latest.yml/badge.svg)](https://github.com/AliverAnme/BBDown/actions/workflows/build_latest.yml)

> Этот проект предназначен исключительно для личного обучения, исследований и некоммерческих целей. Пользователи должны самостоятельно убедиться в соблюдении применимого законодательства при использовании данного инструмента, особенно в отношении авторских прав. Разработчик не несет ответственности за любые споры об авторских правах или юридические последствия, возникающие в результате использования этого инструмента. Используйте инструмент с осторожностью, обеспечивая законность ваших действий.

# BBDown
Консольный загрузчик видео с Bilibili.

# Обратите внимание
Этой программе требуются внешние инструменты для микширования (объединения потоков):

* Обычные видео: [ffmpeg](https://www.gyan.dev/ffmpeg/builds/) или [mp4box](https://gpac.wp.imt.fr/downloads/)
* Dolby Vision: ffmpeg версии 5.0+ или свежая версия mp4box.

# Быстрый старт
Программа опубликована как инструмент [Dotnet Tool](https://www.nuget.org/packages/BBDown/).

Если у вас установлена среда выполнения .NET, вы можете установить BBDown следующей командой:
```
dotnet tool install --global BBDown
```

Для обновления BBDown выполните:
```
dotnet tool update --global BBDown
```

# Загрузки
Релизные версии: https://github.com/AliverAnme/BBDown/releases

Автоматические сборки тестовых версий: https://github.com/AliverAnme/BBDown/actions

# Использование
Выполните `BBDown --help` для вывода полного списка доступных команд и параметров:

```bash
BBDown --help
```

Быстрая справка по основным параметрам:

| Короткий | Длинный параметр | Описание |
|--------|--------|------|
| `-t` | `--use-tv-api` | Использовать TV API для парсинга |
| `-a` | `--use-app-api` | Использовать APP API для парсинга |
| `-I` | `--only-show-info` | Показать только информацию без скачивания |
| `-i` | `--interactive` | Интерактивный выбор качества видеопотоков |
| `-d` | `--download-danmaku` | Скачать даньмаку (комментарии) |
| `-e` | `--encoding-priority` | Приоритет кодеков (например, `hevc,av1,avc`) |
| `-q` | `--dfn-priority` | Приоритет качества видео |
| `-p` | `--select-page` | Выбор серий/частей (например, `-p 1,3,5-10`) |
| `-F` | `--file-pattern` | Шаблон имени файла для сохранения одиночных видео |
| `-M` | `--multi-file-pattern` | Шаблон имени файла для сохранения многосерийных видео |
| `-c` | `--cookie` | Строка Cookie |
| | `--muxer-timeout` | Таймаут микширования в минутах (по умолчанию: 30) |
| | `--retry-count` | Количество попыток при сетевых сбоях (по умолчанию: 3) |
| | `--retry-delay` | Базовая задержка повтора в миллисекундах (по умолчанию: 3000) |
| | `--thread-segment-size` | Размер сегмента при многопоточном скачивании в МБ (по умолчанию: 20) |
| | `--config-file` | Использовать указанный файл конфигурации |

Команды:
- `login` — WEB авторизация через QR-код в приложении Bilibili
- `logintv` — TV авторизация через QR-код в приложении Bilibili
- `serve` — Запуск в режиме API-сервера
  - `-l, --listen` — Адрес прослушивания сервера (по умолчанию: `http://0.0.0.0:23333`)
  - `--max-concurrent` — Максимальное количество одновременных скачиваний (по умолчанию: 3)

# Возможности
- [x] Скачивание аниме и шоу (Web|TV|App)
- [x] Скачивание курсов (Web)
- [x] Скачивание обычных видеороликов (Web|TV|App)
- [x] Парсинг плейлистов, коллекций, избранного и страниц авторов
- [x] Автоматическое скачивание нескольких серий
- [x] Выбор конкретных серий для загрузки
- [x] Интерактивный выбор качества
- [x] Скачивание внешних субтитров с конвертацией в форматы srt/ass
- [x] Автоматическое объединение видео + аудио + субтитров + **глав** `(через ffmpeg или mp4box)`
- [x] Раздельное скачивание видео, аудио или субтитров
- [x] Авторизация через QR-код
- [x] Многопоточное скачивание (с настройкой размера сегментов)
- [x] Поддержка использования aria2c для скачивания
- [x] Поддержка видеокодеков AVC/HEVC/AV1
- [x] **Поддержка скачивания 8K / HDR / Dolby Vision / Dolby Atmos**
- [x] **Дешифрование Widevine DRM (чистая реализация на C#, без Python)**
- [x] Настраиваемый шаблон имени файла для сохранения
- [x] **Режим API-сервера** (`BBDown serve`, поддержка лимитов и лог-файла)
- [x] **Поддержка файлов конфигурации** (`BBDown.config`)
- [x] **Отмена загрузки по Ctrl+C** (передача CancellationToken по всей цепочке)
- [x] **Докачка временных файлов .tmp** (автоматическое возобновление после сбоя)

# Список задач (TODO)

## Завершено ✅

- [x] Ограничение очереди API-сервера (`SemaphoreSlim(3)` для контроля параллелизма)
- [x] Настройка времени жизни соединений DNS (`SocketsHttpHandler.PooledConnectionLifetime = 5min`)
- [x] Таймаут процесса микширования `BBDownMuxer.RunExe` (максимум 30 минут + принудительное завершение)
- [x] Блокировка файлов при параллельной загрузке по путям (`SemaphoreSlim`)
- [x] Уточнение типов исключений (28 блоков generic `Exception` заменены семантическими типами)
- [x] Уточнение стратегии повторов (экспоненциальный бэкoфф + быстрый выход при фатальных ошибках)
- [x] Проброс `CancellationToken` во все методы скачивания (отмена по Ctrl+C / отмена запросов к API)
- [x] Рефакторинг глобального состояния конфигурации (структура `AppSettings` + потокобезопасные блокировки)
- [x] Поддержка докачки `.tmp` (перенос временных файлов, исправление проверок записи)
- [x] Логирование API-сервера в файл (`bbdown-api.log`)
- [x] Безопасные методы доступа к JSON (`JsonElementExtensions`, 10 методов расширения)
- [x] Каркас модульных тестов: `BBDown.Tests` (`BilibiliBvConverterTests` / `UrlResolverTests` / `FormatHelperTests`)
- [x] Разделение основных утилит на `UrlResolver.cs` и `ExternalToolHelper.cs`
- [x] Настраиваемый лимит параллельных загрузок сервера (`--max-concurrent`)
- [x] Определение истекших Cookie и вывод уведомлений ("Требуется авторизация" / "Cookie истек")
- [x] Универсальная безопасная обертка для парсинга JSON (переведено более 200 вызовов)

## В планах 🔴

_Все высокоприоритетные задачи завершены. В дальнейшем возможна оптимизация тестового покрытия, добавление большего количества настроек CLI/HTTP и т.д._

# Руководство пользователя

<details>
<summary>Файл конфигурации (NEW)</summary> 

---

Начиная с версии `1.4.9`, BBDown поддерживает чтение локального файла конфигурации для упрощения ввода команд.

Если параметр `--config-file` не передан, программа по умолчанию ищет файл `BBDown.config` в своем каталоге.

Пример структуры файла конфигурации:
```config
# Это конфигурационный файл программы BBDown
# Строки, начинающиеся с #, игнорируются
# Программа считывает остальные непустые строки: параметры и аргументы должны располагаться на отдельных строках

# Настройка формата имени файлов:
--file-pattern
<videoTitle>[<dfn>]

--multi-file-pattern
<videoTitle>/[P<pageNumberWithZero>]<pageTitle>[<dfn>]

# Интервал в 2 секунды между скачиваниями серий
--delay-per-page
2

# Включение скачивания даньмаку
--download-danmaku
```

</details>

<details>
<summary>Пользовательские шаблоны имен файлов (NEW)</summary> 

---

Начиная с версии `1.4.9`, BBDown позволяет настраивать формат имени выходного файла при микшировании.
| Плейсхолдер | Описание |
|  ----  | ----  |
| `<videoTitle>` | Название видеоролика |
| `<pageNumber>` | Номер серии/части |
| `<pageNumberWithZero>` | Номер серии/части (с ведущими нулями) |
| `<pageTitle>` | Название конкретной серии |
| `<bvid>` | BVID видеоролика |
| `<aid>` | AID видеоролика |
| `<cid>` | CID видеоролика |
| `<dfn>` | Описание качества видеопотока |
| `<res>` | Разрешение видеопотока |
| `<fps>` | Частота кадров видеопотока |
| `<videoCodecs>` | Видеокодек |
| `<videoBandwidth>` | Битрейт видеопотока |
| `<audioCodecs>` | Аудиокодек |
| `<audioBandwidth>` | Битрейт аудиопотока |
| `<ownerName>` | Имя автора видео (пусто для аниме/шоу) |
| `<ownerMid>` | Идентификатор автора MID (пусто для аниме/шоу) |
| `<publishDate>` | Дата публикации (формат: yyyy-MM-dd_HH-mm-ss) |
| `<apiType>` | Тип использованного API (TV/APP/INTL/WEB) |

</details>

<details>
<summary>WEB/TV Авторизация</summary>  

---
  
Авторизация на сайте (WEB) через сканирование QR-кода:
```
BBDown login
```
И следуйте инструкциям на экране.

Авторизация в приложении для Smart TV через сканирование QR-кода:
```
BBDown logintv
```
И следуйте инструкциям на экране.
 
*Примечание: Если при авторизации возникает ошибка `The type initializer for 'Gdip' threw an exception`, обратитесь к теме [#37](https://github.com/AliverAnme/BBDown/issues/37) за решением.*

Использование WEB cookie вручную:
```
BBDown -c "SESSDATA=******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
Использование токена доступа TV вручную:
```
BBDown -tv -token "******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```

</details>

<details>
<summary>APP Авторизация</summary>  

---

> Согласно [#123](https://github.com/AliverAnme/BBDown/issues/123#issuecomment-877583825), сгенерированный при TV-входе `access_token` подходит также для API мобильного приложения (APP). Скопируйте `BBDownTV.data` в файл `BBDownApp.data` для автоматического считывания.

В данный момент программа не умеет получать токен приложения автоматически. Рекомендуется использовать **инструменты анализа трафика (снифферы)** для его получения.

Найдите заголовок `authorization` в запросах приложения. Его значение выглядит как `identify_v1 5227************1`. Токеном (access_key) является строка `5227************1`.

Вы можете передать его вручную с помощью параметра `-token` или сохранить в файл `BBDownApp.data`.
  
```
BBDown -app -token "******" "https://www.bilibili.com/video/BV1qt4y1X7TW"
```

</details>

<details>
<summary>Дешифрование Widevine DRM</summary>

---

BBDown поддерживает **встроенное дешифрование** Widevine CDM, написанное на чистом C#. Программа автоматически получает ключи дешифрования и декодирует защищенный контент Bilibili DRM **без использования Python и библиотеки pywidevine**.

**Подготовка**
1. Получите файл `device.wvd` (файл данных устройства Widevine; его нужно извлечь самостоятельно или взять из доверенного источника).
2. Поместите `device.wvd` в один из следующих каталогов:
   - Папка с исполняемым файлом программы
   - Папка, добавленная в переменную среды `PATH`
   - macOS: `/opt/homebrew/bin` / Linux: `/usr/local/bin` / Windows: Каталог программы

**Использование**
```shell
# Скачивание DRM-защищенного видео (автоматическое дешифрование)
BBDown --decrypt-drm "https://www.bilibili.com/cheese/play/ep1243104"
```

Вы можете вручную указать путь к файлу `device.wvd` с помощью параметров запуска или положив его в рабочий каталог.

**Принцип работы**
- Запрашивает стандартные Widevine потоки с параметром `drm_tech_type=2`.
- Получает ключи от сервера лицензий Bilibili (совместимость зависит от уровня безопасности `security_level` вашего `device.wvd`).
- Декодирует дорожки и собирает их в стандартный MP4-файл.

</details>

<details>
<summary>Часто используемые команды</summary>  

---

Скачивание обычного видеоролика:
```
BBDown "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
Скачивание через TV API (рекомендуется, так как у популярных авторов видеоролики в данном API идут без водяных знаков Bilibili):
```
BBDown -tv "https://www.bilibili.com/video/BV1qt4y1X7TW"
```
При наличии большого количества серий программа скрывает их список по умолчанию. Вы можете отобразить его командой:
```
BBDown --show-all "https://www.bilibili.com/video/BV1At41167aj"
```
Три способа выбора серий для скачивания:
* Одна конкретная серия (например, 10):
```
BBDown "https://www.bilibili.com/video/BV1At41167aj?p=10"
BBDown -p 10 "https://www.bilibili.com/video/BV1At41167aj"
```
* Перечисление серий (например, 1, 2 и 10):
```
BBDown -p 1,2,10 "https://www.bilibili.com/video/BV1At41167aj"
```
* Диапазон серий (например, с 1 по 10):
```
BBDown -p 1-10 "https://www.bilibili.com/video/BV1At41167aj"
```
Скачивание всех серий аниме/шоу:
```
BBDown -p ALL "https://www.bilibili.com/bangumi/play/ss33073"
```

</details>

<details>
<summary>API-сервер</summary>

Запуск сервера (с указанием адреса и порта):

```shell
BBDown serve -l http://0.0.0.0:12450
```

Сервер API не имеет встроенной поддержки протокола HTTPS. При необходимости настройте обратное проксирование через nginx или аналогичные решения.

Полную спецификацию вызовов смотрите в файле [API.md](./API.md).
</details>

# Демонстрация работы
![1](https://user-images.githubusercontent.com/20772925/88686407-a2001480-d129-11ea-8aac-97a0c71af115.gif)

Просмотр скачанного MP4 файла в каталоге:

![2](https://user-images.githubusercontent.com/20772925/88478901-5e1cdc00-cf7e-11ea-97c1-154b9226564e.png)

## Компиляция и сборка

```bash
# Клонирование репозитория
git clone https://github.com/AliverAnme/BBDown.git
cd BBDown

# Восстановление зависимостей и сборка
dotnet restore
dotnet build

# Запуск
BBDown/bin/Debug/net10.0/BBDown --help
```

Инструкции для контрибьюторов смотрите в [CONTRIBUTING.md](./CONTRIBUTING.md).

## История изменений

Подробности смотрите в файле [CHANGELOG.md](./CHANGELOG.md).

## Лицензия

Этот проект распространяется под лицензией [MIT](./LICENSE).

## Безопасность

Пожалуйста, сообщайте об обнаруженных уязвимостях по правилам из файла [SECURITY.md](./SECURITY.md). Не публикуйте уязвимости в открытых Issues.

## Сообщество

- [Руководство контрибьютора](./CONTRIBUTING.md)
- [Кодекс поведения](./CODE_OF_CONDUCT.md)
- [Обсуждения (Discussions)](https://github.com/AliverAnme/BBDown/discussions)

# Благодарности

Этот проект является развитием загрузчика [nilaoda/BBDown](https://github.com/nilaoda/BBDown). Мы благодарим оригинального автора за его неоценимый вклад.

### Дополнительные благодарности ветки:
* https://github.com/spectreconsole/spectre.console

### Благодарности от оригинального автора:
* https://github.com/codebude/QRCoder
* https://github.com/icsharpcode/SharpZipLib
* https://github.com/protocolbuffers/protobuf
* https://github.com/grpc/grpc
* https://github.com/SocialSisterYi/bilibili-API-collect
* https://github.com/SeeFlowerX/bilibili-grpc-api
* https://github.com/FFmpeg/FFmpeg
* https://github.com/gpac/gpac
* https://github.com/aria2/aria2
