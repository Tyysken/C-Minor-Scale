using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Minor_Scale;
using C_Minor_Scale.Controllers;

namespace C_Minor_Scale.Tests.ControllerTests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
