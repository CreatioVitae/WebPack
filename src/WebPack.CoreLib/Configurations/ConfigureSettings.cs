namespace Microsoft.AspNetCore.Builder;

public class ConfigureSettings {
    public bool AuthenticationEnable { get; init; }

    public PathBase? PathBase { get; init; }
}
