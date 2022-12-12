using Microsoft.Extensions.Configuration;
using WebPack.Identity.CoreLib;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService>(this IServiceCollection services, IConfiguration configuration)
        where TBasicAuthenticationService : class, IBasicAuthenticationService =>
        services
            .AddBasicAuthenticationService<TBasicAuthenticationService, BasicAuthenticationDefaultInterceptor>(configuration);

    public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService, TBasicAuthenticationInterceptor>(this IServiceCollection services, IConfiguration configuration)
        where TBasicAuthenticationService : class, IBasicAuthenticationService
        where TBasicAuthenticationInterceptor : class, IBasicAuthenticationInterceptor {
        if (configuration.GetConfigureSettings() is { AuthenticationEnable: false }) {
            return services;
        }

        services.AddScoped<IBasicAuthenticationInterceptor, TBasicAuthenticationInterceptor>();

        services.AddScoped<IBasicAuthenticationService, TBasicAuthenticationService>();

        services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, DefaultBasicAuthenticationHandler>("BasicAuthentication", null);

        return services;
    }
}
