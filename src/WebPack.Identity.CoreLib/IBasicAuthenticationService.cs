using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection {
    public interface IBasicAuthenticationService {
        ValueTask<(bool authenticateResult, IBasicAuthenticatedUser authenticatedUser)> AuthenticateAsync(string username, string password);
    }
}
