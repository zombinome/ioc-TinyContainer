using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyContainer.Tests.TestTypes;

namespace TinyContainer.Tests
{
    [TestClass]
    public class TransientLifetimeManagerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            SimpleDependency.ResetCounters();
        }

        [TestMethod]
        public void TestLifetimeManagerRegisterResolve()
        {
            var lifetimeManager = new TransientLifetimeManager(typeof(SimpleDependency));

            bool keepTrackObject;
            var resolvedObject1 = lifetimeManager.Resolve(null, out keepTrackObject);
            Assert.IsNotNull(resolvedObject1);
            Assert.IsInstanceOfType(resolvedObject1, typeof(SimpleDependency));

            var resolvedObject2 = lifetimeManager.Resolve(null, out keepTrackObject);
            Assert.IsNotNull(resolvedObject2);
            Assert.IsInstanceOfType(resolvedObject2, typeof(SimpleDependency));

            Assert.AreNotSame(resolvedObject1, resolvedObject2);
            Assert.AreEqual(SimpleDependency.ActiveDependecies, 2);

            ((IDisposable)resolvedObject1).Dispose();
            ((IDisposable)resolvedObject2).Dispose();
        }

        [TestMethod]
        public void TestContainerResolveInstance()
        {
            var container = new TinyContainer();
            container.RegisterTransient<SimpleDependency>();

            var dependency1 = container.Resolve<SimpleDependency>();

            Assert.IsNotNull(dependency1);
            Assert.IsInstanceOfType(dependency1, typeof(SimpleDependency));

            Assert.AreEqual(SimpleDependency.ActiveDependecies, 1);
            Assert.AreEqual(SimpleDependency.ResolveCounter, 1);

            var dependency2 = container.Resolve<SimpleDependency>();

            Assert.IsNotNull(dependency2);
            Assert.IsInstanceOfType(dependency2, typeof(SimpleDependency));

            Assert.AreEqual(SimpleDependency.ActiveDependecies, 2);
            Assert.AreEqual(SimpleDependency.ResolveCounter, 2);

            Assert.AreNotSame(dependency1, dependency2);

            container.Dispose();

            Assert.AreEqual(SimpleDependency.ActiveDependecies, 0);
        }
    }
}
