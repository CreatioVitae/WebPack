// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public interface ITokenAuthenticationInterceptor {
    Action<HttpRequest>? InterruptOnBeginnings { get; }

    Func<HttpRequest, (bool interruptResult, string? token)>? InterruptOnCredentials { get; }
}
public class TokenAuthenticationDefaultInterceptor : ITokenAuthenticationInterceptor {
    public Action<HttpRequest>? InterruptOnBeginnings =>
        null;

    public Func<HttpRequest, (bool interruptResult, string? token)>? InterruptOnCredentials =>
        null;
}
