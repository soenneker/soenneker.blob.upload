using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs.Models;

namespace Soenneker.Blob.Upload.Abstract;

/// <summary>
/// All of these methods overwrite a file if it currently exists.. <para/>
/// Scoped IoC
/// </summary>
public interface IBlobUploadUtil
{
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, Stream content, PublicAccessType publicAccessType = PublicAccessType.None);

    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, byte[] bytes, PublicAccessType publicAccessType = PublicAccessType.None);

    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, string content, PublicAccessType publicAccessType = PublicAccessType.None);

    ValueTask<Response<BlobContentInfo>> UploadFromFile(string containerName, string relativeUrl, string absolutePath, PublicAccessType publicAccessType = PublicAccessType.None);

    ValueTask<string> UploadAndGetUri(string container, string fileName, byte[] reportBytes, PublicAccessType publicAccessType = PublicAccessType.None);
}