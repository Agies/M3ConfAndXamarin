using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectLog.Contracts;
using ProjectLog.Core.Controllers;

namespace ProjectLog.UnitTests.ViewController
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _cut;

        [TestInitialize]
        public void Initialize()
        {
            _cut = new HomeController(new MockProxy());
        }

        [TestMethod]
        public void GetActiveProjects_Returns_Result()
        {
            Assert.AreNotEqual(0, _cut.GetActiveProjects());
        }
    }

    public class MockProxy : IProjectProxy
    {
        public Project[] GetActiveProjects()
        {
            return new Project[]
                {
                    new Project(), 
                };
        }
    }
}