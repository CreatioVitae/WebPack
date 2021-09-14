using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection {
    public interface IBasicAuthenticationService {
        ValueTask<(bool authenticateResult, Claim[] authenticatedUserClaims)> AuthenticateAsync(string username, string password);
    }
}
