using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Thrown when the service interface can't be understood.
    /// </summary>
    [Serializable]
    internal class WebServiceCompileException : ApplicationException
    {
        public WebServiceCompileException(string message) : base(message)
        {
        }
    }
}