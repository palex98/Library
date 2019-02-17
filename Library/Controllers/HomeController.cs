using Library.Models;
using Library.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int userId)
        {
            List<LibraryCollection> collection = new List<LibraryCollection>();

            using (var context = new LibraryDBEntities())
            {
                var user = context.User.Where(u => u.Id == userId).FirstOrDefault();

                var listOfUsersBook = context.Loans.Where(u => u.UserId == user.Id);
                ViewBag.UsersBook = listOfUsersBook.ToList();

                var libraries = from l in context.Library group l by l.LibraryId;

                foreach (var lib in libraries)
                {

                    LibraryCollection libraryCollection = new LibraryCollection();

                    libraryCollection.Number = lib.Key;

                    foreach(var bk in lib)
                    {
                        Book book = context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault();

                        int count = context.Library.Where(c => c.BookId == book.Id).FirstOrDefault().Count;

                        BookInLibrary bookInLibrary = new BookInLibrary { Book = book, Count = count };

                        libraryCollection.ListOfBooks.Add(bookInLibrary);
                    }

                    collection.Add(libraryCollection);
                }

                ViewBag.IsAdmin = user.IsAdmin;
            }
            return View(collection);
        }
    }
}