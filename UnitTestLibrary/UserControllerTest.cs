using System;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLibrary
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUserInfoTest()
        {
            var repository = new UserRepository();
            var controller = new UserController(repository);

            var result = controller.GetUserInfo(13);

            Assert.IsNotNull(result);
        }
    }
}
