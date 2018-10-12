using System;
using System.Net.Http;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Make a GET service call.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute(string path = null) : base(HttpMethod.Get, path) { }
    }
}
