using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Authentication;
public static class BasicAuthenticationExtensions {
    const int UserNameIndex = 0;
    const int PasswordIndex = 1;
    const int CredentialValidCount = 2;
    const char CredentialSeparateMarker = ':';

    public static bool TryGetBasicAuthenticationCredential(this HttpRequest request, [NotNullWhen(true)] out string? userName, [NotNullWhen(true)] out string? password, Func<HttpRequest, (bool interruptResult, string? userName, string? password)>? getBasicAuthenticationCredentialsInterrupt = null) {
        userName = null;
        password = null;
        var interruptResult = false;

        if (getBasicAuthenticationCredentialsInterrupt is not null) {
            (interruptResult, userName, password) = getBasicAuthenticationCredentialsInterrupt(request);
        }

        return
            interruptResult ||
            request.Headers.ContainsAuthorizationHeader() &&
            request.Headers.TryGetAuthorizationHeader(out var authHeader) &&
            authHeader.IsBasicAuthentication() &&
            authHeader.TryGetBasicAuthenticationCredential(out userName, out password);
    }

    static bool IsBasicAuthentication(this AuthenticationHeaderValue authorizationHeader) =>
        authorizationHeader.Scheme.Equals(AuthorizationType.Basic, StringComparison.OrdinalIgnoreCase);

    static bool TryGetBasicAuthenticationCredential(this AuthenticationHeaderValue authorizationHeader, [NotNullWhen(true)] out string? userName, [NotNullWhen(true)] out string? password) {
        userName = null;
        password = null;

        return
            authorizationHeader.Parameter is { } nonNullParameter &&
            TryGetBasicAuthorizationCredentialsFromAuthenticationHeaderValueParameter(nonNullParameter, out userName, out password);
    }

    public static bool TryGetBasicAuthorizationCredentialsFromAuthenticationHeaderValueParameter(string nonNullParameter, out string? userName, out string? password) {
        userName = null;
        password = null;

        if (nonNullParameter.TryDecodeUtf8FromBase64String(out var decodedCredential) is false) {
            return false;
        }

        if (decodedCredential.Split([CredentialSeparateMarker], CredentialValidCount) is not { Length: CredentialValidCount } credential) {
            return false;
        }

        userName = credential[UserNameIndex];
        password = credential[PasswordIndex];
        return true;
    }
}
