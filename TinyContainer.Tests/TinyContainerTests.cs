using TinyContainer.Tests.TestTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TinyContainer.Tests
{
    [TestClass]
    public class TinyContainerTests
    {
        [TestMethod]
        public void TestResolitionWithAutowiring()
        {
            var container = new TinyContainer();
            container.RegisterTransient<ITestDependency, SimpleDependency>()
                     .RegisterTransient<CompositeDependecy>();

            var dependency = container.Resolve<CompositeDependecy>();

            Assert.IsNotNull(dependency);
            Assert.IsInstanceOfType(dependency, typeof(CompositeDependecy));

            Assert.IsNotNull(dependency.InnerDependency);
            Assert.IsInstanceOfType(dependency.InnerDependency, typeof(SimpleDependency));

            container.Dispose();

            Assert.IsNull(dependency.InnerDependency); // Checking that dependecny was disposed
        }
    }
}