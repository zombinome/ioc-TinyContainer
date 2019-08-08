using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TinyContainer.Tests.TestTypes;

namespace TinyContainer.Tests
{
    [TestClass]
    public class SingletonLifetimeManagerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            SimpleDependency.ResetCounters();
        }

        [TestMethod]
        public void TestLifetimeManagerRegisterResolve()
        {
            var dependency = new SimpleDependency();

            var lifetimeManager = new SingletonLifetimeManager(dependency);

            bool keepTrackObject;
            var resolvedObject1 = lifetimeManager.Resolve(null, out keepTrackObject);
            Assert.IsNotNull(resolvedObject1);

            var resolvedObject2 = lifetimeManager.Resolve(null, out keepTrackObject);
            Assert.IsNotNull(resolvedObject2);

            Assert.AreSame(resolvedObject1, resolvedObject2);
            Assert.AreEqual(SimpleDependency.ActiveDependecies, 1);

            dependency.Dispose();
        }

        [TestMethod]
        public void TestContainerResolveSingleton()
        {
            var container = new TinyContainer();

            container.RegisterSignleton<SimpleDependency>();

            var result1 = container.Resolve<SimpleDependency>();
            var result2 = container.Resolve<SimpleDependency>();

            Assert.AreSame(result1, result2);
            Assert.AreEqual(SimpleDependency.ActiveDependecies, 1);

            container.Dispose();
        }
    }
}
