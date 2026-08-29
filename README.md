[![](https://img.shields.io/nuget/v/Soenneker.Blob.Upload.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Upload/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.upload/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blob.upload/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blob.Upload.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Upload/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.upload/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blob.upload/actions/workflows/codeql.yml)

# Soenneker.Blob.Upload

All of these methods overwrite a file if it currently exists.

## Install

```bash
dotnet add package Soenneker.Blob.Upload
```

## Quick start

```csharp
using Soenneker.Blob.Upload.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddBlobUploadUtilAsSingleton();
```

Registers Blob Upload Util with a singleton lifetime.

## What you get

- `IBlobUploadUtil` — All of these methods overwrite a file if it currently exists.
- `BlobUploadUtilRegistrar` — A utility library for Azure Blob storage upload operations.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IBlobUploadUtil.Upload(containerName, relativeUrl, bytes, contentType, publicAccessType, cancellationToken)` | Uploads a byte array to the specified container and relative URL. | A task whose result is the requested response. |
| `IBlobUploadUtil.Upload(containerName, relativeUrl, content, contentType, publicAccessType, cancellationToken)` | Uploads a string content to the specified container and relative URL. | A task whose result is the requested response. |
| `IBlobUploadUtil.UploadFromFile(containerName, relativeUrl, absolutePath, contentType, publicAccessType, cancellationToken)` | Uploads a file from the specified absolute path to the specified container and relative URL. | A task whose result is the requested response. |
| `IBlobUploadUtil.UploadAndGetSasUri(container, fileName, bytes, contentType, publicAccessType, cancellationToken)` | Uploads a byte array to the specified container and file name, and returns a SAS URI to access the uploaded blob. | A task whose result is the text returned by upload And Get Sas URI. |
| `BlobUploadUtilRegistrar.AddBlobUploadUtilAsSingleton(services)` | Registers Blob Upload Util with a singleton lifetime. | The same service collection, so additional registrations can be chained. |
| `BlobUploadUtilRegistrar.AddBlobUploadUtilAsScoped(services)` | Registers Blob Upload Util with a scoped lifetime. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
