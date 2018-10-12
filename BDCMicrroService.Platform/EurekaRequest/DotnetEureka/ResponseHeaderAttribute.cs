using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// The out parameter or return value marked with this attribute will be assigned
    /// the value from the response.
    /// </summary>
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class ResponseHeaderAttribute : Attribute
    {
        public ResponseHeaderAttribute(string headerName)
        {
            Header = headerName;
        }

        public string Header { get; }
    }
}
