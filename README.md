[![](https://img.shields.io/nuget/v/Soenneker.Blob.Upload.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Upload/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.upload/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blob.upload/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blob.Upload.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Upload/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.upload/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blob.upload/actions/workflows/codeql.yml)

# Soenneker.Blob.Upload

Uploads files, streams, bytes, and text to Azure block blobs.

## Installation

```bash
dotnet add package Soenneker.Blob.Upload
```

## Configuration

```json
{
  "Environment": "Production",
  "Azure": {
    "Storage": {
      "Blob": {
        "ConnectionString": "<connection string>",
        "AccountName": "<storage account name>",
        "AccountKey": "<storage account key>"
      }
    }
  }
}
```

The connection string is used for uploads. The account name, account key, and environment are required by the registered SAS utility and by `UploadAndGetSasUri`.

## Registration

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blob.Upload.Registrars;

services.AddBlobUploadUtilAsSingleton();
```

`AddBlobUploadUtilAsScoped()` is also available.

## Upload text or bytes

```csharp
using Soenneker.Blob.Upload.Abstract;

public sealed class ManifestStore
{
    private readonly IBlobUploadUtil _uploads;

    public ManifestStore(IBlobUploadUtil uploads)
    {
        _uploads = uploads;
    }

    public async ValueTask Save(
        string json,
        CancellationToken cancellationToken)
    {
        _ = await _uploads.Upload(
            "assets",
            "manifests/latest.json",
            json,
            contentType: "application/json",
            cancellationToken: cancellationToken);
    }
}
```

## Upload a file or stream

Use the file and stream overloads to avoid first copying large content into a byte array:

```csharp
_ = await uploads.UploadFromFile(
    "exports",
    "daily/archive.zip",
    absolutePath,
    contentType: "application/zip",
    cancellationToken: cancellationToken);

await using FileStream input = File.OpenRead(absolutePath);

_ = await uploads.Upload(
    "exports",
    "daily/archive.zip",
    input,
    contentType: "application/zip",
    cancellationToken: cancellationToken);
```

The stream overload begins at the stream's current position and does not dispose or rewind it.

## Upload and create a read URL

```csharp
string sasUri = await uploads.UploadAndGetSasUri(
    "private-assets",
    "previews/report.pdf",
    pdfBytes,
    contentType: "application/pdf",
    cancellationToken: cancellationToken);
```

The returned URL is read-only and expires after one month. Treat it as a credential: do not log it or expose it to callers that should not read the blob.

## Behavior

- Every upload overwrites an existing block blob at the same container and path. This API does not expose conditional create or ETag protection.
- Supplying `contentType` stores it in the blob's HTTP headers. The library does not infer a MIME type from a filename.
- Byte-array and string overloads buffer content in a temporary memory stream. Prefer file or stream uploads for large or untrusted payloads.
- The caller owns streams passed to `Upload`; the library-owned streams used by byte and string overloads are disposed automatically.
- The underlying client utility creates a missing container. `publicAccessType` applies only during that creation and does not change an existing container's access level.
- Azure service failures are thrown as `RequestFailedException`; cancellation is passed through to client creation and upload.
