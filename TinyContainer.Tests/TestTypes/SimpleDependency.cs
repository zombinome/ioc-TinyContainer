using System;
using System.Threading;

namespace TinyContainer.Tests.TestTypes
{
    public class SimpleDependency : IDisposable, ITestDependency
    {
        private static int resolveCounter;
        private static int activeDependecies;

        private bool disposed;

        private static readonly object locker = new object();

        public SimpleDependency()
        {
            Interlocked.Increment(ref resolveCounter);
            Interlocked.Increment(ref activeDependecies);
        }

        public void Dispose()
        {
            lock (locker)
            {
                if (disposed)
                {
                    return;
                }

                disposed = true;
            }

            Interlocked.Decrement(ref activeDependecies);
        }

        public static int ResolveCounter
        {
            get
            {
                return Interlocked.Add(ref resolveCounter, 0);
            }
        }

        public static int ActiveDependecies
        {
            get
            {
                return Interlocked.Add(ref activeDependecies, 0);
            }
        }

        public static void ResetCounters()
        {
            Interlocked.CompareExchange(ref activeDependecies, 0, activeDependecies);
            Interlocked.CompareExchange(ref resolveCounter, 0, resolveCounter);
        }
    }
}
