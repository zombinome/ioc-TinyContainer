using System;
using System.Collections.Generic;

namespace TinyContainer
{
    public class TinyContainer: IDisposable
    {
        private readonly Dictionary<Type, ILifetimeManager> registrations = new Dictionary<Type, ILifetimeManager>();

        private readonly Dictionary<object, ILifetimeManager> objectsToRelease = new Dictionary<object, ILifetimeManager>();

        public void Register(Type dependencyType, ILifetimeManager lifetimeManager)
        {
            registrations[dependencyType] = lifetimeManager;
        }

        public object Resolve(Type serviceType)
        {
            if (registrations.TryGetValue(serviceType, out ILifetimeManager lifetimeManager))
            {
                var dependency = lifetimeManager.Resolve(this, out bool shouldRelease);
                if (dependency != null)
                {
                    if (shouldRelease)
                    {
                        objectsToRelease.Add(dependency, lifetimeManager);
                    }

                    return dependency;
                }
            }

            throw new FailedToResolveDependencyException(serviceType, "Unknown service type. Unable to resolve.");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}