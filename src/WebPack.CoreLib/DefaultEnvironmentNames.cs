namespace Microsoft.AspNetCore.Hosting;
public static class DefaultEnvironmentNames {
    public const string Development = nameof(Development);

    public const string DevelopmentRemote = nameof(DevelopmentRemote);

    public const string Staging = nameof(Staging);

    public const string Production = nameof(Production);
}

public static class DefaultEnvironment {
    public static string GetEnvironmentName() =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? DefaultEnvironmentNames.Production;

    public static bool IsDevelopment() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Development;

    public static bool IsDevelopmentRemote() =>
        GetEnvironmentName() is DefaultEnvironmentNames.DevelopmentRemote;

    public static bool IsStaging() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Staging;

    public static bool IsProduction() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Production;
}
