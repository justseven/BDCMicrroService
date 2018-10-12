using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Mark the relative route to find the service.  Use this if you
    /// are not using a discovery service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string baseRoute = null)
        {
            BaseRoute = baseRoute ?? string.Empty;
        }

        public string BaseRoute { get; }
    }
}
