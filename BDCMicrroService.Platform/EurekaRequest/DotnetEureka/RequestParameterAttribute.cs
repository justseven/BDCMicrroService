﻿using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// The value of the method parameter marked with this attribute will
    /// be be applied to the specified parameter name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class RequestParameterAttribute : Attribute
    {
        public RequestParameterAttribute(string paramName = null)
        {
            Parameter = paramName;
        }

        public string Parameter { get; }
    }
}
