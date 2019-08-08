using System;

namespace TinyContainer
{
    public interface ILifetimeManager : IDisposable
    {
        object Resolve(TinyContainer container, out bool shoudRelease);

        void Release(object dependency);
    }
}