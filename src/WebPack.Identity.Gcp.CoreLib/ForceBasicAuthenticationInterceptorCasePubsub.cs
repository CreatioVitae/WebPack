using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public class ForceBasicAuthenticationInterceptorCasePubSub : IBasicAuthenticationInterceptor {
    public static readonly string QueryKeyName = "force-basic-authentication-case-pub-sub-key";

    public static (bool interruptResult, string? userName, string? password) Interrupt(HttpRequest httpRequest) {
        if (httpRequest.Query.TryGetValue(QueryKeyName, out var queryVal) is false || queryVal.FirstOrDefault() is not { } nonNull) {
            return (false, null, null);
        }

        return
            BasicAuthenticationExtensions.TryGetBasicAuthorizationCredentialsFromAuthenticationHeaderValueParameter(nonNull, out var userName, out var password)
                ? (true, userName, password)
                : (false, null, null);
    }

    public Func<HttpRequest, (bool interruptResult, string? userName, string? password)>? InterruptOnCredentials { get; } = Interrupt;
}
