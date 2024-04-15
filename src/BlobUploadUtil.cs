using System.IO;
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

///<inheritdoc cref="IBlobUploadUtil"/>
public class BlobUploadUtil : IBlobUploadUtil
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
    
    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, Stream content, PublicAccessType publicAccessType = PublicAccessType.None)
    {
        _logger.LogInformation("Uploading Blob to container ({containerName}), path {relativeUrl} ...", containerName, relativeUrl);
        BlobClient blobClient = await _blobClientUtil.Get(containerName, relativeUrl, publicAccessType).NoSync();

        Response<BlobContentInfo> response = await blobClient.UploadAsync(content, overwrite: true).NoSync();

        _logger.LogDebug("Finished Blob upload to container ({containerName}), path {relativeUrl}", containerName, relativeUrl);

        return response;
    }

    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, byte[] bytes, PublicAccessType publicAccessType = PublicAccessType.None)
    {
        using MemoryStream stream = await _memoryStreamUtil.Get(bytes).NoSync();
        Response<BlobContentInfo> result = await Upload(containerName, relativeUrl, stream, publicAccessType).NoSync();
        return result;
    }

    public async ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, string content, PublicAccessType publicAccessType = PublicAccessType.None)
    {
        using MemoryStream stream = await _memoryStreamUtil.Get(content).NoSync();
        Response<BlobContentInfo> result = await Upload(containerName, relativeUrl, stream, publicAccessType).NoSync();
        return result;
    }

    public async ValueTask<Response<BlobContentInfo>> UploadFromFile(string containerName, string relativeUrl, string absolutePath, PublicAccessType publicAccessType = PublicAccessType.None)
    {
        _logger.LogInformation("Uploading Blob ({absolutePath}) to container ({containerName}) at {relativeUrl} ...", absolutePath, containerName, relativeUrl);
        BlobClient blobClient = await _blobClientUtil.Get(containerName, relativeUrl, publicAccessType).NoSync();

        Response<BlobContentInfo> response = await blobClient.UploadAsync(absolutePath, overwrite: true).NoSync();

        _logger.LogDebug("Finished upload Blob ({absolutePath}) to container ({containerName}) at {relativeUrl}", absolutePath, containerName, relativeUrl);

        return response;
    }

    public async ValueTask<string> UploadAndGetUri(string container, string fileName, byte[] reportBytes, PublicAccessType publicAccessType = PublicAccessType.None)
    {
        _ = await Upload(container, fileName, reportBytes, publicAccessType).NoSync();

        string uri = (await _blobSasUtil.GetSasUriWithClient(container, fileName).NoSync())!;

        return uri;
    }
}