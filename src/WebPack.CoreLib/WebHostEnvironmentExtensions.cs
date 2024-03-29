using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace WebPack;
public static class WebHostEnvironmentExtensions {
    public static bool IsDevelopmentRemote(this IWebHostEnvironment webHostEnvironment) =>
        webHostEnvironment.EnvironmentName == DefaultEnvironmentNames.DevelopmentRemote;
}
