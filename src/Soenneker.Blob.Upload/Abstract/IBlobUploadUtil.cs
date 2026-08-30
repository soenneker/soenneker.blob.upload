using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs.Models;

namespace Soenneker.Blob.Upload.Abstract;

/// <summary>
/// Uploads content to Azure block blobs, overwriting an existing blob at the same path.
/// </summary>
public interface IBlobUploadUtil
{
    /// <summary>
    /// Uploads a byte array to a block blob.
    /// </summary>
    /// <param name="containerName">Name of the destination container.</param>
    /// <param name="relativeUrl">Path of the destination blob.</param>
    /// <param name="bytes">Content to upload.</param>
    /// <param name="contentType">Optional MIME type stored in the blob's HTTP headers.</param>
    /// <param name="publicAccessType">Public access level used if the container must be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The Azure upload response.</returns>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, byte[] bytes, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads text to a block blob.
    /// </summary>
    /// <param name="containerName">Name of the destination container.</param>
    /// <param name="relativeUrl">Path of the destination blob.</param>
    /// <param name="content">Text to upload.</param>
    /// <param name="contentType">Optional MIME type stored in the blob's HTTP headers.</param>
    /// <param name="publicAccessType">Public access level used if the container must be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The Azure upload response.</returns>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, string content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a local file to a block blob.
    /// </summary>
    /// <param name="containerName">Name of the destination container.</param>
    /// <param name="relativeUrl">Path of the destination blob.</param>
    /// <param name="absolutePath">Path of the local file to upload.</param>
    /// <param name="contentType">Optional MIME type stored in the blob's HTTP headers.</param>
    /// <param name="publicAccessType">Public access level used if the container must be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The Azure upload response.</returns>
    ValueTask<Response<BlobContentInfo>> UploadFromFile(string containerName, string relativeUrl, string absolutePath, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a byte array and returns a read-only SAS URL for the resulting blob.
    /// </summary>
    /// <param name="container">Name of the destination container.</param>
    /// <param name="fileName">Path of the destination blob.</param>
    /// <param name="bytes">Content to upload.</param>
    /// <param name="contentType">Optional MIME type stored in the blob's HTTP headers.</param>
    /// <param name="publicAccessType">Public access level used if the container must be created.</param>
    /// <param name="cancellationToken">Token used to cancel the upload.</param>
    /// <returns>A read-only SAS URL that expires after one month.</returns>
    ValueTask<string> UploadAndGetSasUri(string container, string fileName, byte[] bytes, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads the remaining content of a stream to a block blob.
    /// </summary>
    /// <param name="containerName">Name of the destination container.</param>
    /// <param name="relativeUrl">Path of the destination blob.</param>
    /// <param name="content">Readable stream positioned at the first byte to upload. The caller retains ownership.</param>
    /// <param name="contentType">Optional MIME type stored in the blob's HTTP headers.</param>
    /// <param name="publicAccessType">Public access level used if the container must be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The Azure upload response.</returns>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, Stream content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);
}
