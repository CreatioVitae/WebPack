using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public static class OptionsConfigurationServiceCollectionExtensions {
    public static IServiceCollection ConfigureIfDevelopment<TOptions>(this IServiceCollection serviceDescriptors, IConfigurationSection configurationSection) where TOptions : class =>
        DefaultWebEnvironment.WebApps.GetEnvironmentName() != DefaultEnvironmentNames.Development
        ? serviceDescriptors
        : serviceDescriptors.Configure<TOptions>(configurationSection);

    public static IServiceCollection AddOptionsIfDevelopment<TOptions>(this IServiceCollection serviceDescriptors, IConfigurationSection configurationSection) where TOptions : class {
        if (DefaultWebEnvironment.WebApps.GetEnvironmentName() != DefaultEnvironmentNames.Development) {
            return serviceDescriptors;
        }

        serviceDescriptors.AddOptions<TOptions>().Bind(configurationSection).ValidateDataAnnotations();

        return serviceDescriptors;
    }
}
