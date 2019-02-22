using System;
using System.Web.Mvc;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLibrary
{
    [TestClass]
    public class RegistrationControllerTest
    {
        [TestMethod]
        public void IndexTestNotNull()
        {
            var userRepository = new UserRepository();

            var controller = new RegistrationController(userRepository);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            var userRepository = new UserRepository();

            var controller = new RegistrationController(userRepository);

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Registration", result.ViewName);
        }
    }
}
