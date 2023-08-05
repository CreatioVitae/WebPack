using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net.Http;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc;
public class HttpPurgeAttribute : HttpMethodAttribute {
    private static readonly IEnumerable<string> SupportedMethods = new[] { ExtendedHttpMethod.PurgeMethodName };

    /// <summary>
    /// Creates a new <see cref="HttpGetAttribute"/>.
    /// </summary>
    public HttpPurgeAttribute()
        : base(SupportedMethods) {
    }

    /// <summary>
    /// Creates a new <see cref="HttpGetAttribute"/> with the given route template.
    /// </summary>
    /// <param name="template">The route template. May not be null.</param>
    public HttpPurgeAttribute([StringSyntax("Route")] string template)
        : base(SupportedMethods, template) {
        if (template == null) {
            throw new ArgumentNullException(nameof(template));
        }
    }
}
