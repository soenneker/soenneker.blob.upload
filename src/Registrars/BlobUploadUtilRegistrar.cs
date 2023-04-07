using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blob.Client.Registrars;
using Soenneker.Blob.Sas.Registrars;
using Soenneker.Blob.Upload.Abstract;
using Soenneker.Utils.MemoryStream.Registrars;

namespace Soenneker.Blob.Upload.Registrars;

/// <summary>
/// A utility library for Azure Blob storage upload operations
/// </summary>
public static class BlobUploadUtilRegistrar
{
    public static void AddBlobUploadUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IBlobUploadUtil, BlobUploadUtil>();
        services.AddMemoryStreamUtil();
        services.AddBlobClientUtilAsSingleton();
        services.AddBlobSasUtilAsSingleton();
    }

    /// <summary>
    /// Recommended
    /// </summary>
    public static void AddBlobUploadUtilAsScoped(this IServiceCollection services)
    {
        services.AddMemoryStreamUtil();
        services.AddBlobClientUtilAsScoped();
        services.TryAddScoped<IBlobUploadUtil, BlobUploadUtil>();
        services.AddBlobSasUtilAsScoped();
    }
}