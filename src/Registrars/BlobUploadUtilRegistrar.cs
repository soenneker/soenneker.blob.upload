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
    public static IServiceCollection AddBlobUploadUtilAsSingleton(this IServiceCollection services)
    {
        services.AddMemoryStreamUtil();
        services.AddBlobSasUtilAsSingleton();
        services.TryAddSingleton<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }

    public static IServiceCollection AddBlobUploadUtilAsScoped(this IServiceCollection services)
    {
        services.AddMemoryStreamUtil();
        services.AddBlobSasUtilAsScoped();
        services.TryAddScoped<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }
}