using Microsoft.AspNetCore.Hosting;

namespace WebPack {
    public static class WebHostEnvironmentExtensions {
        public static bool IsDevelopmentRemote(this IWebHostEnvironment webHostEnvironment) {
            return (webHostEnvironment.EnvironmentName == DefaultEnvironmentNames.DevelopmentRemote);
        }
    }
}