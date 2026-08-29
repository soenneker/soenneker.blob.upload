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
    /// Registers Blob Upload Util with a singleton lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBlobUploadUtilAsSingleton(this IServiceCollection services)
    {
        services.AddMemoryStreamUtilAsSingleton()
                .AddBlobSasUtilAsSingleton();
        services.TryAddSingleton<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }

    /// <summary>
    /// Registers Blob Upload Util with a scoped lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBlobUploadUtilAsScoped(this IServiceCollection services)
    {
        services.AddMemoryStreamUtilAsSingleton()
                .AddBlobSasUtilAsScoped();
        services.TryAddScoped<IBlobUploadUtil, BlobUploadUtil>();

        return services;
    }
}
