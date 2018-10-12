using System;

namespace DotnetEureka
{
    /// <inheritdoc />
    /// <summary>
    /// Marks a service with it's name.  Use this with a discovery
    /// service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class DotnetEurekaClientAttribute : Attribute
    {
        public DotnetEurekaClientAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
