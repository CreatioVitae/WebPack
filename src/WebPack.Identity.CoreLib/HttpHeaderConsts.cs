namespace Microsoft.AspNetCore.Authentication {
    public static class HttpHeaderConsts {
        public const string AuthorizationHeaderKey = "Authorization";
    }

    public static class AuthorizationType {
        public const string Basic = "basic";

        public const string Bearer = "bearer";

        public const string Digest = "digest";
    }
}
