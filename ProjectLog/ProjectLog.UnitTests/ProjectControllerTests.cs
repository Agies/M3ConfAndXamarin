using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectLog.Web.Controllers;

namespace ProjectLog.UnitTests
{
    [TestClass]
    public class ProjectControllerTests
    {
        private ProjectController _cut;

        [TestInitialize]
        public void Initialize()
        {
            _cut = new ProjectController();
        }

        [TestMethod]
        public void Get_Returns_All_Projects()
        {
            Assert.AreNotEqual(0, _cut.Get().Count());
        }
    }
}
