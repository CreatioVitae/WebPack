using System.Net.Http.Headers;
using static Microsoft.AspNetCore.Authentication.HttpHeaderConsts;

internal static class AuthenticationCoreExtensions {

    internal static bool ContainsAuthorizationHeader(this IHeaderDictionary headers) =>
        headers.ContainsKey(AuthorizationHeaderKey);

    internal static bool TryGetAuthorizationHeader(this IHeaderDictionary headers, [NotNullWhen(true)] out AuthenticationHeaderValue? authorizationHeader) =>
        AuthenticationHeaderValue.TryParse(headers[AuthorizationHeaderKey], out authorizationHeader);
}
