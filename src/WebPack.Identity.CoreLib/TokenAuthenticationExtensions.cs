using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Authentication;
public static class TokenAuthenticationExtensions {
    public static bool TryGetTokenAuthenticationCredential(this HttpRequest request, [NotNullWhen(true)] out string? token, Func<HttpRequest, (bool interruptResult, string? token)>? getTokenAuthenticationCredentialsInterrupt = null) {
        token = null;

        var interruptResult = false;

        if (getTokenAuthenticationCredentialsInterrupt is not null) {
            (interruptResult, token) = getTokenAuthenticationCredentialsInterrupt(request);
        }

        return
            interruptResult ||
            request.Headers.ContainsAuthorizationHeader() &&
            request.Headers.TryGetAuthorizationHeader(out var authHeader) &&
            authHeader.IsTokenAuthentication() &&
            authHeader.TryGetTokenAuthenticationCredential(out token);
    }

    static bool IsTokenAuthentication(this AuthenticationHeaderValue authorizationHeader) =>
        authorizationHeader.Scheme.Equals(AuthorizationType.Bearer, StringComparison.OrdinalIgnoreCase);

    static bool TryGetTokenAuthenticationCredential(this AuthenticationHeaderValue authorizationHeader, [NotNullWhen(true)] out string? token) {
        switch (authorizationHeader.Parameter) {
            case not null:
                token = authorizationHeader.Parameter;
                return true;
            default:
                token = null;
                return false;
        }
    }
}
