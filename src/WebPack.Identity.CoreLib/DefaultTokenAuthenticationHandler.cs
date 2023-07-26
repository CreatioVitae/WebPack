using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebPack.Identity.CoreLib;
public class DefaultTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    readonly ITokenAuthenticationService _tokenAuthenticationService;
    private readonly ITokenAuthenticationInterceptor _tokenAuthenticationInterceptor;

    public DefaultTokenAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ITokenAuthenticationService tokenAuthenticationService,
        ITokenAuthenticationInterceptor tokenAuthenticationInterceptor
    ) : base(options, logger, encoder, clock) =>
        (_tokenAuthenticationService, _tokenAuthenticationInterceptor) = (tokenAuthenticationService, tokenAuthenticationInterceptor);

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
        _tokenAuthenticationInterceptor.InterruptOnBeginnings?.Invoke(Request);

        if (Context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is not null) {
            return AuthenticateResult.NoResult();
        }

        if (Request.TryGetTokenAuthenticationCredential(out var token, _tokenAuthenticationInterceptor.InterruptOnCredentials) is false) {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        var (authenticateResult, claims) = await _tokenAuthenticationService.AuthenticateAsync(token);

        if (authenticateResult is false) {
            return AuthenticateResult.Fail($"Invalid {nameof(token)}.");
        }

        var ticket = new AuthenticationTicket(
            new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name)),
            Scheme.Name
        );

        return AuthenticateResult.Success(ticket);
    }
}
