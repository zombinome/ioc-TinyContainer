using System;
using System.Collections.Generic;

namespace TinyContainer
{
    public class TinyContainer: IDisposable
    {
        private readonly Dictionary<Type, ILifetimeManager> registrations = new Dictionary<Type, ILifetimeManager>();

        public void Register(Type dependencyType, ILifetimeManager lifetimeManager)
        {
            registrations[dependencyType] = lifetimeManager;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}