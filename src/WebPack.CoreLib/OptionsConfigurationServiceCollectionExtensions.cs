using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

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

    public static T GetAvailable<T>(this IConfiguration configuration) {
        var obj = configuration.Get<T>();

        ArgumentNullException.ThrowIfNull(obj);

        Validator.ValidateObject(obj, new ValidationContext(obj), true);
        return obj;
    }
}
