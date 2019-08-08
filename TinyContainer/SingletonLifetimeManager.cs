using System;

namespace TinyContainer
{
    public class SingletonLifetimeManager : ILifetimeManager
    {
        protected object dependency = null;

        protected readonly bool isDisposable = false;

        protected Type dependencyType = null;

        protected bool resolved = false;

        public SingletonLifetimeManager(object serviceInstance)
        {
            dependency = serviceInstance;
            resolved = true;
            isDisposable = serviceInstance is IDisposable;
        }

        public SingletonLifetimeManager(Type serviceType)
        {
            dependencyType = serviceType;
            resolved = false;
            isDisposable = typeof(IDisposable).IsAssignableFrom(serviceType);
        }

        public object Resolve(TinyContainer container, out bool shoudRelease)
        {
            shoudRelease = false;
            if (!resolved)
            {
                var constructor = TypeHelper.GetConstructor(dependencyType);
                dependency = TypeHelper.CreateInstance(container, constructor);
                resolved = true;
            }

            return dependency;
        }

        public void Release(object dependency)
        {
        }

        public void Dispose()
        {
            if (resolved && isDisposable)
            {
                ((IDisposable)dependency).Dispose();
            }
        }
    }
}
