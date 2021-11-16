using System.Net.Http.Headers;

namespace System.Net.Http;
public static class HttpRequestMessageExtensions {
    public static HttpRequestMessage ApplyBasicAuthorizationHeader(this HttpRequestMessage requestMessage, (string userName, string password) credential) {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic",
            $"{credential.userName}:{credential.password}".EncodeBase64String()
        );

        return requestMessage;
    }
}
