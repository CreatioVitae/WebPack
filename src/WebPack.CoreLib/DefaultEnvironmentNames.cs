using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Hosting;

public static class DefaultWebEnvironment {
    public static IDefaultEnvironmentAccessor WebApps { get; } = new WebDefaultEnvironmentAccessor();
}

public class WebDefaultEnvironmentAccessor : IDefaultEnvironmentAccessor {
    public string GetEnvironmentName() =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? DefaultEnvironmentNames.Production;

    public bool IsDevelopment() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Development;

    public bool IsNotDevelopment() =>
        IsDevelopment() is false;

    public bool IsDevelopmentRemote() =>
        GetEnvironmentName() is DefaultEnvironmentNames.DevelopmentRemote;

    public bool IsNotDevelopmentRemote() =>
        IsDevelopmentRemote() is false;

    public bool IsStaging() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Staging;

    public bool IsNotStaging() =>
        IsStaging() is false;

    public bool IsProduction() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Production;

    public bool IsNotProduction() =>
        IsProduction() is false;
}
