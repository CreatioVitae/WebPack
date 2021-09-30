# WebPack.Identity.CoreLib
Identity Pack For ASP.NET Core

## use Basic Authentication

### Add IBasicAuthenticationService
```
public class HogeBasicAuthenticationService : IBasicAuthenticationService {
    //Use ORMIntegrator（ctor Injection...）
    readonly SqlManager<HogeContext> _sqlManager;

    public HogeBasicAuthenticationService(SqlManager<HogeContext> sqlManager) =>
        _sqlManager = sqlManager;

    public async ValueTask<(bool authenticateResult, Claim[] authenticatedUserClaims)> AuthenticateAsync(string username, string password) {
        if (await _sqlManager.DbContext.MstHoges.FirstOrDefaultAsync(e => e.UserId == username && e.Password == password) is not MstPartner authenticatedUser) {
            return (authenticateResult: false, authenticatedUserClaims: System.Array.Empty<Claim>());
        }

        return (
            true,
            new[] {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser.PartnerId.ToString()),
                new Claim(ClaimTypes.Name, authenticatedUser.UserId),
                new Claim(ClaimTypes.Email, authenticatedUser.Email),
                // Extend Claim Setting Point...
            }
        );
    }
}
```

### Add Service(And HogeBasicAuthenticationService DI Setting)
```
public void ConfigureServices(IServiceCollection services) {
    //...

    services.AddBasicAuthenticationService<HogeBasicAuthenticationService>();
}
```

### Configure
```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    // Always write UseAuthentication before UseAuthorization
    app.UseAuthentication();

    app.UseAuthorization();
}
```