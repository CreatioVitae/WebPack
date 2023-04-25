// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public interface ITokenAuthenticationInterceptor {
    Func<HttpRequest, (bool interruptResult, string? token)>? InterruptOnCredentials { get; }
}
public class TokenAuthenticationDefaultInterceptor : ITokenAuthenticationInterceptor {
    public Func<HttpRequest, (bool interruptResult, string? token)>? InterruptOnCredentials =>
        null;
}
