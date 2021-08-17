using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using static Microsoft.AspNetCore.Authentication.HttpHeaderConsts;

namespace Microsoft.AspNetCore.Authentication {
    public static class BasicAuthenticationExtensions {
        const int userNameIndex = 0;
        const int passwordIndex = 1;
        const int credentialValidCount = 2;
        const char credentialSeparateMarker = ':';

        public static bool TryGetBasicAuthenticationCredential(this HttpRequest request, [NotNullWhen(true)] out string? userName, [NotNullWhen(true)] out string? password) {
            userName = null;
            password = null;

            return
                request.Headers.ContainsAuthorizationHeader() &&
                request.Headers.TryGetAuthorizationHeader(out var authHeader) &&
                authHeader.IsBasicAuthentication() &&
                authHeader.TryGetBasicAuthenticationCredential(out userName, out password);
        }

        static bool ContainsAuthorizationHeader(this IHeaderDictionary headers) =>
            headers.ContainsKey(AuthorizationHeaderKey);

        static bool TryGetAuthorizationHeader(this IHeaderDictionary headers, [NotNullWhen(true)] out AuthenticationHeaderValue? authorizationHeader) =>
            AuthenticationHeaderValue.TryParse(headers[AuthorizationHeaderKey], out authorizationHeader);

        static bool IsBasicAuthentication(this AuthenticationHeaderValue authorizationHeader) =>
            authorizationHeader.Scheme.Equals(AuthorizationType.Basic, StringComparison.OrdinalIgnoreCase);

        static bool TryGetBasicAuthenticationCredential(this AuthenticationHeaderValue authorizationHeader, [NotNullWhen(true)] out string? userName, [NotNullWhen(true)] out string? password) {
            userName = null;
            password = null;

            if (authorizationHeader.Parameter is null || authorizationHeader.Parameter.TryDecodeUtf8FromBase64String(out var decodedCredential) is false) {
                return false;
            }

            if (decodedCredential.Split(new[] { credentialSeparateMarker }, credentialValidCount) is not string[] credential || credential.Length is not credentialValidCount) {
                return false;
            }

            userName = credential[userNameIndex];
            password = credential[passwordIndex];
            return true;
        }

    }
}
