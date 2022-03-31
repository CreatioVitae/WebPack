using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.Configuration;
public static class ConfigurationExtensions {
    public static ConfigureSettings? GetConfigureSettings(this IConfiguration configuration) =>
    configuration.GetSection(nameof(ConfigureSettings)).Get<ConfigureSettings>();
}
