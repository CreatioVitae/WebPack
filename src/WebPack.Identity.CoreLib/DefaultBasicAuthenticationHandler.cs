using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebPack.Identity.CoreLib;
public class DefaultBasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    readonly IBasicAuthenticationService _basicAuthenticationService;
    private readonly IBasicAuthenticationInterceptor _basicAuthenticationInterceptor;

    public DefaultBasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IBasicAuthenticationService basicAuthenticationService,
        IBasicAuthenticationInterceptor basicAuthenticationInterceptor
    ) : base(options, logger, encoder, clock) =>
        (_basicAuthenticationService, _basicAuthenticationInterceptor) = (basicAuthenticationService, basicAuthenticationInterceptor);

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
        if (Context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is not null) {
            return AuthenticateResult.NoResult();
        }

        if (Request.TryGetBasicAuthenticationCredential(out var userName, out var password, _basicAuthenticationInterceptor.InterruptOnCredentials) is false) {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        var (authenticateResult, claims) = await _basicAuthenticationService.AuthenticateAsync(userName, password);

        if (authenticateResult is false) {
            return AuthenticateResult.Fail("Invalid Username or Password");
        }

        var ticket = new AuthenticationTicket(
            new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name)),
            Scheme.Name
        );

        return AuthenticateResult.Success(ticket);
    }
}
