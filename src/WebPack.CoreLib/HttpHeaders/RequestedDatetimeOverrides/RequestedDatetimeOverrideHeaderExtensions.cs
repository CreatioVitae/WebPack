using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace WebPack.CoreLib.HttpHeaders.RequestedDatetimeOverrides;

public class HeaderKeys {
    public const string ForceOverrideRequestedDatetime = "x-force-override-requested-datetime";
    public const string OverrideRequestedDatetime = "x-override-requested-datetime";
}

public static class RequestedDatetimeOverrideHeaderExtensions {
    public static bool GetForceOverrideRequestedDatetimeFromHeader(this HttpContext context, bool canFallBack = true, bool defaultOption = false) =>
        context.GetBoolValueFromHeader(canFallBack, defaultOption, HeaderKeys.ForceOverrideRequestedDatetime, nameof(HeaderKeys.ForceOverrideRequestedDatetime));

    public static DateTime? GetOverrideRequestedDatetimeFromHeader(this HttpContext context, bool canFallBack = true, DateTime? defaultOption = null) =>
        context.GetDatetimeValueFromHeader(canFallBack, defaultOption, HeaderKeys.OverrideRequestedDatetime, nameof(HeaderKeys.OverrideRequestedDatetime));
}
