// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public interface IBasicAuthenticationInterceptor {
    Func<HttpRequest, (bool interruptResult, string? userName, string? password)>? InterruptOnCredentials { get; }
}
public class BasicAuthenticationDefaultInterceptor : IBasicAuthenticationInterceptor {
    public Func<HttpRequest, (bool interruptResult, string? userName, string? password)>? InterruptOnCredentials =>
        null;
}
