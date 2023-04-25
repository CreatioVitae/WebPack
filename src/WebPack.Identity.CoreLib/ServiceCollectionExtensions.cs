using Microsoft.Extensions.Configuration;
using WebPack.Identity.CoreLib;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions {
    static class DefaultSchemeNames {
        internal const string BasicAuth = "BasicAuthentication";

        internal const string TokenAuth = "TokenAuthentication";
    }

    public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService>(this IServiceCollection services, IConfiguration configuration, string schemeName = DefaultSchemeNames.BasicAuth)
        where TBasicAuthenticationService : class, IBasicAuthenticationService =>
        services
            .AddBasicAuthenticationService<TBasicAuthenticationService, BasicAuthenticationDefaultInterceptor>(configuration, schemeName);

    public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService, TBasicAuthenticationInterceptor>(this IServiceCollection services, IConfiguration configuration, string schemeName = DefaultSchemeNames.BasicAuth)
        where TBasicAuthenticationService : class, IBasicAuthenticationService
        where TBasicAuthenticationInterceptor : class, IBasicAuthenticationInterceptor {
        if (configuration.GetConfigureSettings() is { AuthenticationEnable: false }) {
            return services;
        }

        services.AddScoped<IBasicAuthenticationInterceptor, TBasicAuthenticationInterceptor>();

        services.AddScoped<IBasicAuthenticationService, TBasicAuthenticationService>();

        services.AddAuthentication(schemeName).AddScheme<AuthenticationSchemeOptions, DefaultBasicAuthenticationHandler>(schemeName, null);

        return services;
    }

    public static IServiceCollection AddTokenAuthenticationService<TTokenAuthenticationService>(this IServiceCollection services, IConfiguration configuration, string schemeName = DefaultSchemeNames.TokenAuth)
        where TTokenAuthenticationService : class, ITokenAuthenticationService =>
        services
            .AddTokenAuthenticationService<TTokenAuthenticationService, TokenAuthenticationDefaultInterceptor>(configuration, schemeName);

    public static IServiceCollection AddTokenAuthenticationService<TTokenAuthenticationService, TTokenAuthenticationInterceptor>(this IServiceCollection services, IConfiguration configuration, string schemeName = DefaultSchemeNames.TokenAuth)
        where TTokenAuthenticationService : class, ITokenAuthenticationService
        where TTokenAuthenticationInterceptor : class, ITokenAuthenticationInterceptor {
        if (configuration.GetConfigureSettings() is { AuthenticationEnable: false }) {
            return services;
        }

        services.AddScoped<ITokenAuthenticationInterceptor, TTokenAuthenticationInterceptor>();

        services.AddScoped<ITokenAuthenticationService, TTokenAuthenticationService>();

        services.AddAuthentication(schemeName).AddScheme<AuthenticationSchemeOptions, DefaultBasicAuthenticationHandler>(schemeName, null);

        return services;
    }
}
