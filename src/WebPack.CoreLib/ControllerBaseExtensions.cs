// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc;

public static class ControllerBaseExtensions {
    public static string GetActionName(this ControllerBase controllerBase, string actionNameOrigin) =>
        actionNameOrigin.Remove("Async");
}
