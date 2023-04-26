using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace System.Net.Http;
public static class HttpRequestMessageExtensions {
    public static HttpRequestMessage ApplyBasicAuthorizationHeader(this HttpRequestMessage requestMessage, (string userName, string password) credential) {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic",
            $"{credential.userName}:{credential.password}".EncodeBase64String()
        );

        return requestMessage;
    }

    public static HttpRequestMessage ApplyTokenAuthorizationHeader(this HttpRequestMessage requestMessage, string token) {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
            AuthorizationType.Bearer,
            token
        );

        return requestMessage;
    }
}
