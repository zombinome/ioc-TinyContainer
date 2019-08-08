using System;

namespace TinyContainer
{
    [Serializable]
    internal class FailedToResolveDependencyException : Exception
    {
        public FailedToResolveDependencyException(Type serviceType, string message, Exception innerException = null): base(message, innerException)
        {
            this.ServiceType = serviceType;
        }

        public Type ServiceType { get; set; }

    }
}