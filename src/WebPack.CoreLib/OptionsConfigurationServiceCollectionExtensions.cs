using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection {
    public static class OptionsConfigurationServiceCollectionExtensions {
        public static IServiceCollection ConfigureIfDevelopment<TOptions>(this IServiceCollection serviceDescriptors, IConfigurationSection configurationSection) where TOptions : class =>
            DefaultEnvironment.GetEnvironmentName() != DefaultEnvironmentNames.Development
            ? serviceDescriptors
            : serviceDescriptors.Configure<TOptions>(configurationSection);
    }
}
