using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;

namespace Microsoft.AspNetCore.Authentication {
    public static class HttpRequestExtensions {
        public static bool TryGetBasicAuthenticationCredential(this HttpRequest request, out string? userName, out string? password) {
            if (request.Headers.ContainsKey("Authorization") is false) {
                userName = null;
                password = null;
                return false;
            }

            if (AuthenticationHeaderValue.TryParse(request.Headers["Authorization"], out var authHeader) is false) {
                userName = null;
                password = null;
                return false;
            };

            if (authHeader.Parameter is not string || authHeader.Parameter.TryDecodeUtf8FromBase64String(out var credentialStringForBasicAuth) is false) {
                userName = null;
                password = null;
                return false;
            }

            var credential = credentialStringForBasicAuth.Split(new[] { ':' }, 2);

            if (credential.Length != 2) {
                userName = null;
                password = null;
                return false;
            }

            userName = credential[0];
            password = credential[1];
            return true;
        }

    }
}
