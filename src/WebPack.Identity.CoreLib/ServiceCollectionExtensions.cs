namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddBasicAuthenticationService<TBasicAuthenticationService>(this IServiceCollection services) where TBasicAuthenticationService : class, IBasicAuthenticationService =>
            services.AddScoped<IBasicAuthenticationService, TBasicAuthenticationService>();
    }
}
