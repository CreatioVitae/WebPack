using Serilog;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;
public static class ConfigurationBuilderExtensions {
    //Note:https://github.com/serilog/serilog-aspnetcore/blob/dev/samples/EarlyInitializationSample/Program.cs
    public static IConfigurationRoot CreateDefaultConfiguration(ConfigurationBuilder configurationBuilder) =>
        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{DefaultWebEnvironment.WebApps.GetEnvironmentName()}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

    public static ILogger CreateDefaultLogger(this ConfigurationBuilder configurationBuilder) =>
        new LoggerConfiguration().ReadFrom.Configuration(CreateDefaultConfiguration(configurationBuilder)).CreateLogger();
}
