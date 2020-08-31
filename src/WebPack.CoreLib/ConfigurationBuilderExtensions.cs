﻿using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.IO;

namespace Microsoft.Extensions.Configuration {
    public static class ConfigurationBuilderExtensions {
        public static ILogger CreateDefaultLogger(this ConfigurationBuilder configurationBuilder) {
            static IConfigurationRoot CreateDefaultConfigurationRoot(ConfigurationBuilder configurationBuilder) {
                return configurationBuilder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{DefaultEnvironment.GetEnvironmentName()}.json")
                    .Build();
            }

            return new LoggerConfiguration().ReadFrom.Configuration(CreateDefaultConfigurationRoot(configurationBuilder)).CreateLogger();
        }
    }
}