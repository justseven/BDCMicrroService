using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// The value of this method parameter marked with this attribute will
    /// be supplied to the header identified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class RequestHeaderAttribute : Attribute
    {
        public RequestHeaderAttribute(string headerName)
        {
            Header = headerName;
        }

        public string Header { get; }
    }
}
