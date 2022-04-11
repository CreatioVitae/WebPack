namespace Microsoft.AspNetCore.Http;

public static class PathStringExtensions {
    static readonly char[] Slash = { '/' };

    public static string WithoutLeadingSlash(this PathString pathString, bool forceWithTrailingSlash) {
        ArgumentNullException.ThrowIfNull(pathString.Value);

        return pathString.Value.TrimStart(Slash).WithTrailingSlash(forceWithTrailingSlash);
    }

    static string WithTrailingSlash(this string s, bool forceWithTrailingSlash) =>
        forceWithTrailingSlash
            ? $"{s}/"
            : s;
}
