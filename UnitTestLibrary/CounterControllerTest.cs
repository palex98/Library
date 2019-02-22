using System;
using Library.Controllers;
using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLibrary
{
    [TestClass]
    public class CounterControllerTest
    {
        [TestMethod]
        public void GetListOfLibrariesNotNull()
        {
            var repository = new LibraryRepository();
            var controller = new LibraryController(repository);

            var result1 = controller.GetLibraries(1);
            var result2 = controller.GetLibraries(2);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
        }
    }
}
