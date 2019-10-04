using Microsoft.AspNetCore.Hosting;

namespace WebPack {
    public static class HostingEnvironmentExtensions {
        public static bool IsDevelopmentRemote(this IHostingEnvironment hostingEnvironment) {
            return hostingEnvironment.IsEnvironment(DefaultEnvironmentNames.DevelopmentRemote);
        }
    }
}