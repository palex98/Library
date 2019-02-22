using System;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLibrary
{
    [TestClass]
    public class BookControllerTest
    {
        [TestMethod]
        public void TestGetUsers13BookNotNull()
        {
            var repository = new BookRepository();
            var controller = new BookController(repository);

            var result = controller.GetUsersBook(13);

            Assert.IsNotNull(result);
        }
    }
}
