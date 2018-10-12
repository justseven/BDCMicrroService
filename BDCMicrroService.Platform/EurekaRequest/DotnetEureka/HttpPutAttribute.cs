using System;
using System.Net.Http;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Make a PUT service call.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpPutAttribute : HttpMethodAttribute
    {
        public HttpPutAttribute(string path = null) : base(HttpMethod.Put, path) { }
    }
}
