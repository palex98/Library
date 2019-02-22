using System;
using System.Web.Mvc;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLibrary
{
    [TestClass]
    public class SignUpTest
    {
        [TestMethod]
        public void IndexTestNotNull()
        {
            var userRepository = new UserRepository();

            var controller = new SignUpController(userRepository);

            var result = controller.SignUp("log", "pass") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
