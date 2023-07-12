using Microsoft.Extensions.Primitives;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Http;

public static class HttpHeaderExtensions {
    public static string? GetHeaderValue(this HttpContext? context, string headerKey) {
        StringValues values = default;
        return context?.Request.Headers.TryGetValue(headerKey, out values) switch {
            true =>
                values.ToString() is var rawValues && !string.IsNullOrWhiteSpace(rawValues)
                    ? rawValues
                    : default,
            false => default,
            _ => default
        };
    }

    public static bool GetBoolValueFromHeader(this HttpContext context, bool canFallBack, bool defaultOption, string headerKey, string headerName) =>
        context.GetHeaderValue(headerKey) is not { } headerValue
            ? canFallBack ? defaultOption : throw new ArgumentNullException($"{headerName} header is not available.")
            : bool.TryParse(headerValue, out var result)
                ? result
                : canFallBack
                    ? defaultOption
                    : throw new InvalidCastException($"{headerName} header is not available.");

    public static DateTime? GetDatetimeValueFromHeader(this HttpContext context, bool canFallBack, DateTime? defaultOption, string headerKey, string headerName) =>
        context.GetHeaderValue(headerKey) is not { } headerValue
            ? canFallBack ? defaultOption : throw new ArgumentNullException($"{headerName} header is not available.")
            : DateTime.TryParse(headerValue, out var result)
                ? result
                : canFallBack
                    ? defaultOption
                    : throw new InvalidCastException($"{headerName} header is not available.");
}
