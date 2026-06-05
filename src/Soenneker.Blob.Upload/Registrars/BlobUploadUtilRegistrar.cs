using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blob.Sas.Registrars;
using Soenneker.Blob.Upload.Abstract;
using Soenneker.Utils.MemoryStream.Registrars;

namespace Soenneker.Blob.Upload.Registrars;

/// <summary>
/// A utility library for Azure Blob storage upload operations
/// </summary>
public static class BlobUploadUtilRegistrar
{
    /// <summary>
    /// Adds blob upload util as singleton.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddBlobUploadUtilAsSingleton(this IServiceCollection services)
    {
        services.AddMemoryStreamUtilAsSingleton()
                .AddBlobSasUtilAsSingleton();
        services.TryAddSingleton<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }

    /// <summary>
    /// Adds blob upload util as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddBlobUploadUtilAsScoped(this IServiceCollection services)
    {
        services.AddMemoryStreamUtilAsSingleton()
                .AddBlobSasUtilAsScoped();
        services.TryAddScoped<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }
}