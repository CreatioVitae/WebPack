using System.Security.Claims;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public interface IBasicAuthenticationService {
    ValueTask<(bool authenticateResult, Claim[] authenticatedUserClaims)> AuthenticateAsync(string username, string password);
}
