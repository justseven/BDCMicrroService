using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Used to mark the method parameter as the value for the path
    /// variable in the HttpMethodAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class PathValueAttribute : Attribute
    {
        public PathValueAttribute(string param = null)
        {
            Variable = param;
        }

        public string Variable { get; }
    }
}
