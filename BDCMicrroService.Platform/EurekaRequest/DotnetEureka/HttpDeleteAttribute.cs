using System;
using System.Net.Http;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Make a DELETE service call.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public HttpDeleteAttribute(string path = null) : base(HttpMethod.Delete, path) { }
    }
}
