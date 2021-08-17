using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;

namespace Microsoft.AspNetCore.Authentication {
    public static class HttpRequestExtensions {
        const int userNameIndex = 0;
        const int passwordIndex = 1;

        public static bool TryGetBasicAuthenticationCredential(this HttpRequest request, out string? userName, out string? password) {
            static bool CreateFailResult(out string? userName, out string? password) {
                userName = null;
                password = null;
                return false;
            }

            if (request.Headers.ContainsKey("Authorization") is false) {
                return CreateFailResult(out userName, out password);
            }

            if (AuthenticationHeaderValue.TryParse(request.Headers["Authorization"], out var authHeader) is false) {
                return CreateFailResult(out userName, out password);
            };

            if (authHeader.Parameter is null || authHeader.Parameter.TryDecodeUtf8FromBase64String(out var decodedCredential) is false) {
                return CreateFailResult(out userName, out password);
            }

            if (decodedCredential.Split(new[] { ':' }, 2) is not string[] credential || credential.Length is not 2) {
                return CreateFailResult(out userName, out password);
            }

            userName = credential[userNameIndex];
            password = credential[passwordIndex];
            return true;


        }

    }
}
