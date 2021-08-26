namespace Microsoft.Extensions.DependencyInjection {
    public interface IBasicAuthenticatedUser {
        public int Id { get; init; }

        public string Username { get; init; }
    }
}
