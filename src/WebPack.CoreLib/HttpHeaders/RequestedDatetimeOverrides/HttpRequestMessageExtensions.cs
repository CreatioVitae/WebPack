using System.Collections.Generic;
using WebPack.CoreLib.HttpHeaders.RequestedDatetimeOverrides;

// ReSharper disable once CheckNamespace
namespace System.Net.Http;
public static class HttpRequestMessageExtensions {
    public static HttpRequestMessage ApplyRequestedDatetimeOverrideHeaders(this HttpRequestMessage requestMessage, DateTime overrideValue, string requestedDatetimeFormat) {
        static IEnumerable<string> CreateOverrideRequestedDatetimeHeaderValues(DateTime overrideValue, string requestedDatetimeFormat) {
            yield return overrideValue.ToString(requestedDatetimeFormat);
        }

        requestMessage.Headers.Add(HeaderKeys.ForceOverrideRequestedDatetime, new []{ bool.TrueString });
        requestMessage.Headers.Add(HeaderKeys.OverrideRequestedDatetime, CreateOverrideRequestedDatetimeHeaderValues(overrideValue, requestedDatetimeFormat));
        return requestMessage;
    }
}
