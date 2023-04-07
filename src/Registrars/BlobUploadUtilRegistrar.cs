using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blob.Client.Registrars;
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
        services.AddMemoryStreamUtil();
        services.AddBlobClientUtilAsSingleton();
        services.TryAddSingleton<IBlobUploadUtil, BlobUploadUtil>();
    }

    /// <summary>
    /// Recommended
    /// </summary>
    public static void AddBlobUploadUtilAsScoped(this IServiceCollection services)
    {
        services.AddMemoryStreamUtil();
        services.AddBlobClientUtilAsScoped();
        services.TryAddScoped<IBlobUploadUtil, BlobUploadUtil>();
    }
}
