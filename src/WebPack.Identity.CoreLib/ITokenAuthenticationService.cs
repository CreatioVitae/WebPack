using System.Security.Claims;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public interface ITokenAuthenticationService {
    ValueTask<(bool authenticateResult, Claim[] authenticatedUserClaims)> AuthenticateAsync(string token);
}
