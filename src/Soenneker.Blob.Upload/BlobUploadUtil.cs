using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Soenneker.Blob.Client.Abstract;
using Soenneker.Blob.Sas.Abstract;
using Soenneker.Blob.Upload.Abstract;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.MemoryStream.Abstract;

namespace Soenneker.Blob.Upload;

/// <inheritdoc cref="IBlobUploadUtil"/>
public sealed class BlobUploadUtil : IBlobUploadUtil
{
    private readonly IBlobClientUtil _blobClientUtil;
    private readonly ILogger<BlobUploadUtil> _logger;
    private readonly IMemoryStreamUtil _memoryStreamUtil;
    private readonly IBlobSasUtil _blobSasUtil;

    public BlobUploadUtil(IBlobClientUtil blobClientUtil, ILogger<BlobUploadUtil> logger, IMemoryStreamUtil memoryStreamUtil, IBlobSasUtil blobSasUtil)
    {
        _blobClientUtil = blobClientUtil;
        _logger = logger;
        _memoryStreamUtil = memoryStreamUtil;
        _blobSasUtil = blobSasUtil;
    }

    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, byte[] bytes, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using MemoryStream stream = await _memoryStreamUtil.Get(bytes, cancellationToken).NoSync();
        return await Upload(containerName, relativeUrl, stream, contentType, publicAccessType, cancellationToken).NoSync();
    }

    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, string content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using MemoryStream stream = await _memoryStreamUtil.Get(content, cancellationToken).NoSync();
        return await Upload(containerName, relativeUrl, stream, contentType, publicAccessType, cancellationToken).NoSync();
    }

    public async ValueTask<Response<BlobContentInfo>> UploadFromFile(string containerName, string relativeUrl, string absolutePath, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Uploading file to blob {containerName}/{relativeUrl} ...", containerName, relativeUrl);
        BlobClient blobClient = await _blobClientUtil.Get(containerName, relativeUrl, publicAccessType, cancellationToken).NoSync();

        BlobHttpHeaders? blobHttpHeaders = GetBlobHeaders(contentType);

        Response<BlobContentInfo> response = await blobClient.UploadAsync(absolutePath, blobHttpHeaders, cancellationToken: cancellationToken).NoSync();

        _logger.LogDebug("Finished file upload to blob {containerName}/{relativeUrl}", containerName, relativeUrl);

        return response;
    }

    public async ValueTask<string> UploadAndGetSasUri(string container, string fileName, byte[] bytes, string? contentType = null, PublicAccessType publicAccessType = PublicAccessType.None,
        CancellationToken cancellationToken = default)
    {
        _ = await Upload(container, fileName, bytes, contentType, publicAccessType, cancellationToken).NoSync();

        return _blobSasUtil.GetSasUri(container, fileName);
    }

    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, Stream content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Uploading Blob to container ({containerName}), path {relativeUrl} ...", containerName, relativeUrl);
        BlobClient blobClient = await _blobClientUtil.Get(containerName, relativeUrl, publicAccessType, cancellationToken).NoSync();

        BlobHttpHeaders? blobHttpHeaders = GetBlobHeaders(contentType);

        Response<BlobContentInfo> response = await blobClient.UploadAsync(content, blobHttpHeaders, cancellationToken: cancellationToken).NoSync();

        _logger.LogDebug("Finished Blob upload to container ({containerName}), path {relativeUrl}", containerName, relativeUrl);

        return response;
    }

    private static BlobHttpHeaders? GetBlobHeaders(string? contentType)
    {
        if (contentType == null)
            return null;

        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = contentType
        };

        return blobHttpHeaders;
    }
}
