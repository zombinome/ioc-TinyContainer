using System;
using System.Reflection;

namespace TinyContainer
{
    public class TransientLifetimeManager : ILifetimeManager
    {
        protected readonly Type dependencyType;

        protected readonly bool isDisposable;

        protected readonly ConstructorInfo constructor = null;

        public TransientLifetimeManager(Type serviceType)
        {
            dependencyType = serviceType;
            constructor = TypeHelper.GetConstructor(serviceType);
            isDisposable = typeof(IDisposable).IsAssignableFrom(serviceType);
        }

        public object Resolve(TinyContainer container, out bool shouldRelease)
        {
            shouldRelease = isDisposable;
            return TypeHelper.CreateInstance(container, constructor);
        }

        public void Release(object dependency)
        {
            if (dependency is IDisposable)
            {
                ((IDisposable)dependency).Dispose();
            }
        }

        public void Dispose()
        {
        }
    }
}
