using Library.Models;
using Library.Models.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int userId)
        {
            var userFromCookies = HttpContext.Request.Cookies["user"].Value;

            if(userFromCookies != userId.ToString())
            {
                return Redirect("/");
            }

            List<LibraryCollection> collection = new List<LibraryCollection>();

            using (var context = new LibraryDBEntities())
            {
                var user = context.User.Where(u => u.Id == userId).FirstOrDefault();

                var listOfUsersBook = context.Loan.Where(u => u.UserId == user.Id);
                ViewBag.UsersBook = listOfUsersBook.ToList();

                var libraries = context.Library;

                foreach (var lib in libraries)
                {

                    LibraryCollection libraryCollection = new LibraryCollection();

                    libraryCollection.Title = lib.Title;

                    var books = context.LibraryItem.Where(l => l.LibraryId == lib.Id);

                    foreach(var bk in books)
                    {
                        Book book = context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault();

                        int count = context.LibraryItem.Where(c => c.BookId == book.Id).FirstOrDefault().Count;

                        BookInLibrary bookInLibrary = new BookInLibrary { Book = book, Count = count };

                        libraryCollection.ListOfBooks.Add(bookInLibrary);
                    }

                    collection.Add(libraryCollection);
                }

                ViewBag.IsAdmin = user.IsAdmin;

                HttpContext.Response.Cookies["user"].Value = userId.ToString();

                ViewBag.UserName = user.Name;
            }

            return View(collection);
        }
    }
}