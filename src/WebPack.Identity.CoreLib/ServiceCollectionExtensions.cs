using Microsoft.AspNetCore.Authentication;
using WebPack.Identity.CoreLib;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService>(this IServiceCollection services) where TBasicAuthenticationService : class, IBasicAuthenticationService {
            services.AddScoped<IBasicAuthenticationService, TBasicAuthenticationService>();

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, DefaultBasicAuthenticationHandler>("BasicAuthentication", null);

            return services;
        }
    }
}
