// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public class ConfigureSettings {
    public bool AuthenticationEnable { get; init; }

    public PathBase? PathBase { get; init; }

    public bool IgnoreForceRedirectsToHttps { get; init; } = false;

    public bool ResponseCompressionEnable { get; init; } = false;
}
