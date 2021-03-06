﻿using System;
using System.Net.Http;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Use this to mark service calls with non-standard HTTP methods.
    /// This is also the base class for all the standard HTTP methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpMethodAttribute : Attribute
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public HttpMethodAttribute(HttpMethod method, string path)
        {
            Method = method;
            Path = path ?? string.Empty;
        }

        public string Path { get; }
        public HttpMethod Method { get; }
    }
}
