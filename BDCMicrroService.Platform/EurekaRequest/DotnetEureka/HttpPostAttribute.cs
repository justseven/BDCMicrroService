using System;
using System.Net.Http;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Make a POST service call.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute(string path = null) : base(HttpMethod.Post, path) { }
    }
}
