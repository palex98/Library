using System;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace UnitTestLibrary
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTestNotNull()
        {
            var userRepository = new UserRepository();
            var libraryRepository = new LibraryRepository();

            var controller = new HomeController(libraryRepository, userRepository);

            var result = controller.Index(2);
        }
    }
}
