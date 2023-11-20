// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Hosting;

public static class WebHostBuilderExtensions {
    public static IWebHostBuilder UseCloudRun(this IWebHostBuilder webBuilder) {
        AppContext.SetSwitch("System.Net.SocketsHttpHandler.Http3Support", false);

        static string GetCloudRunInternalUrl() =>
            !int.TryParse(Environment.GetEnvironmentVariable("PORT"), out var port)
                ? throw new InvalidCastException($"環境変数から取得した{nameof(port)}がintにキャスト出来ませんでした。")
                : new UriBuilder(Uri.UriSchemeHttp, "0.0.0.0", port).ToString();

        return webBuilder.UseUrls(GetCloudRunInternalUrl());
    }
}
