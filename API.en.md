[English](API.en.md) | [Русский](API.ru.md) | [简体中文](API.md)

# JSON API Documentation

## API

If BBDown is started in server mode, it will start an HTTP server locally with the following APIs:

### Get Task List
**Endpoint:** `/get-tasks/`

**Method:** GET

**Description:** Retrieves a list of all tasks, including currently running and completed tasks.

**Response:** JSON-formatted `DownloadTaskCollection`.

### Get Running Task List
**Endpoint:** `/get-tasks/running`

**Method:** GET

**Description:** Retrieves a list of all currently running tasks.

**Response:** JSON-formatted `List<DownloadTask>`, representing the list of running tasks.

### Get Completed Task List
**Endpoint:** `/get-tasks/finished`

**Method:** GET

**Description:** Retrieves a list of all completed tasks.

**Response:** JSON-formatted `List<DownloadTask>`, representing the list of completed tasks.

### Get Specific Task
**Endpoint:** `/get-tasks/{id}`

**Method:** GET

**Description:** Retrieves details of a specific task based on the video's AID.

**Parameters:**
- `{id}` (Path Parameter): The video's AID.

**Response:** If a matching task is found, it returns a JSON-formatted `DownloadTask`. If not found, it returns `404 Not Found`.

### Add Task
**Endpoint:** `/add-task`

**Method:** POST

**Description:** Adds a new task to the task list.

**Request Body:** JSON-formatted task options complying with the `MyOption` structure. It is not required to carry every single field of `MyOption`; having only the `Url` field is sufficient.

**Response:**
- Returns `200 OK` if the request is valid and the task was added successfully.
- Returns `400 Bad Request` with an error message `"输入有误"` (invalid input) if the request is invalid.

### Remove All Completed Tasks
**Endpoint:** `/remove-finished`

**Method:** GET

**Description:** Removes all completed tasks from the records.

**Response:**
- Returns `200 OK`.

### Remove Failed Completed Tasks
**Endpoint:** `/remove-finished/failed`

**Method:** GET

**Description:** Removes all completed tasks that failed (`IsSuccessful == false`).

**Response:**
- Returns `200 OK`.

### Remove Specific Completed Task
**Endpoint:** `/remove-finished/{id}`

**Method:** GET

**Description:** Removes a specific completed task based on the video's AID.

**Parameters:**
- `{id}` (Path Parameter): The video's AID.

**Response:**
- Returns `200 OK` regardless of whether a matching task ID is found.

## Data Structures

### `DownloadTask` Structure
The `DownloadTask` structure represents information about a download task.

**Properties:**
- `Aid` `<string>`: The resolved Aid of the video, used as a unique identifier for running tasks. Duplicate values are allowed among completed tasks.
- `Url` `<string>`: The URL requested for the download task. The program supports raw Bilibili links as well as CLI-supported formats like `av|bv|BV|ep|ss`.
- `TaskCreateTime` `<long>`: Task creation time (Unix timestamp in seconds, local timezone).
- `Title` `<string?>`: Title of the video.
- `Pic` `<string?>`: URL link to the cover image of the video.
- `VideoPubTime` `<long?>`: Video publishing time (Unix timestamp in seconds).
- `TaskFinishTime` `<long?>`: Task completion time (Unix timestamp in seconds, local timezone).
- `Progress` `<double>`: Download progress of the task, represented as a decimal between `0.0` and `1.0`.
- `DownloadSpeed` `<double>`: Download speed in Bytes per second. For running tasks, this represents the real-time speed of the last update. For completed tasks, this represents the average speed.
- `TotalDownloadedBytes` `<double>`: Total downloaded bytes. Note that the final number may be slightly smaller than the actual file size.
- `IsSuccessful` `<bool>`: Identifies whether the task completed successfully.

### `DownloadTaskCollection` Structure
The `DownloadTaskCollection` structure contains two lists representing running and completed tasks.

**Properties:**
- `Running` `<List<DownloadTask>>`: A list of currently running tasks.
- `Finished` `<List<DownloadTask>>`: A list of completed tasks.

### `MyOption` Structure

Please refer to [BBDown/MyOption.cs](./BBDown/MyOption.cs). The properties map almost one-to-one with command-line options; simply supply the same values you would pass to the CLI. This structure may evolve across versions. Refer to the file in your specific version.

### Notes
- Due to limits in BBDown's download progress reporting frequency, the final `TotalDownloadedBytes` might be slightly lower than the actual file size (equivalent to about 1 second of download speed). For very small files, this deviation can be more noticeable.
- There is currently no robust mechanism inside BBDown's engine to cancel a single download task. Once submitted, tasks run until they either complete or fail.
- Currently, the server does not enforce limits on the number of simultaneous active tasks. If you add tasks frequently in a short period, a significant number of downloads will run concurrently. Be careful not to exhaust system resources. A download task queue might be implemented in the future.
- The API server does not support native HTTPS. If needed, please set up a reverse proxy using nginx or a similar tool.
- In server mode, task lists are protected by thread-safe locks for concurrent access, but the global credentials in `Config` are shared across concurrent tasks.

### Examples

#### Add Task by BVID

```shell
curl -X POST -H 'Content-Type: application/json' -d '{ "Url": "BV1qt4y1X7TW" }' http://localhost:58682/add-task
```

#### Download to Specific Directory

Windows:
```shell
curl -X POST -H 'Content-Type: application/json' -d '{ "Url": "BV1qt4y1X7TW", "FilePattern": "C:\\Downloads\\<videoTitle>[<dfn>]" }' http://localhost:58682/add-task
```

Unix-Like:
```shell
curl -X POST -H 'Content-Type: application/json' -d '{ "Url": "BV1qt4y1X7TW", "FilePattern": "/Downloads/<videoTitle>[<dfn>]" }' http://localhost:58682/add-task
```
