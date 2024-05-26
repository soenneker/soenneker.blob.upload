using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs.Models;

namespace Soenneker.Blob.Upload.Abstract;

/// <summary>
/// All of these methods overwrite a file if it currently exists.. <para/>
/// </summary>
public interface IBlobUploadUtil
{
    /// <summary>
    /// Uploads a byte array to the specified container and relative URL.
    /// </summary>
    /// <param name="containerName">The name of the container where the blob will be uploaded.</param>
    /// <param name="relativeUrl">The relative URL where the blob will be uploaded.</param>
    /// <param name="bytes">The byte array to be uploaded as a blob.</param>
    /// <param name="contentType">The content type of the blob. Optional.</param>
    /// <param name="publicAccessType">The public access type for the blob. Defaults to None.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Optional.</param>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, byte[] bytes, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a string content to the specified container and relative URL.
    /// </summary>
    /// <param name="containerName">The name of the container where the blob will be uploaded.</param>
    /// <param name="relativeUrl">The relative URL where the blob will be uploaded.</param>
    /// <param name="content">The string content to be uploaded as a blob.</param>
    /// <param name="contentType">The content type of the blob. Optional.</param>
    /// <param name="publicAccessType">The public access type for the blob. Defaults to None.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Optional.</param>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, string content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file from the specified absolute path to the specified container and relative URL.
    /// </summary>
    /// <param name="containerName">The name of the container where the blob will be uploaded.</param>
    /// <param name="relativeUrl">The relative URL where the blob will be uploaded.</param>
    /// <param name="absolutePath">The absolute path of the file to be uploaded as a blob.</param>
    /// <param name="contentType">The content type of the blob. Optional.</param>
    /// <param name="publicAccessType">The public access type for the blob. Defaults to None.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Optional.</param>
    ValueTask<Response<BlobContentInfo>> UploadFromFile(string containerName, string relativeUrl, string absolutePath, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a byte array to the specified container and file name, and returns a SAS URI to access the uploaded blob.
    /// </summary>
    /// <param name="container">The name of the container where the blob will be uploaded.</param>
    /// <param name="fileName">The name of the file to be uploaded as a blob.</param>
    /// <param name="bytes">The byte array to be uploaded as a blob.</param>
    /// <param name="contentType">The content type of the blob. Optional.</param>
    /// <param name="publicAccessType">The public access type for the blob. Defaults to None.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Optional.</param>
    ValueTask<string> UploadAndGetSasUri(string container, string fileName, byte[] bytes, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a stream content to the specified container and relative URL.
    /// </summary>
    /// <param name="containerName">The name of the container where the blob will be uploaded.</param>
    /// <param name="relativeUrl">The relative URL where the blob will be uploaded.</param>
    /// <param name="content">The stream content to be uploaded as a blob.</param>
    /// <param name="contentType">The content type of the blob. Optional.</param>
    /// <param name="publicAccessType">The public access type for the blob. Defaults to None.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Optional.</param>
    ValueTask<Response<BlobContentInfo>> Upload(string containerName, string relativeUrl, Stream content, string? contentType = null,
        PublicAccessType publicAccessType = PublicAccessType.None, CancellationToken cancellationToken = default);
}