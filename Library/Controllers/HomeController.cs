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

            if (userFromCookies != userId.ToString())
            {
                return Redirect("/");
            }

            List<LibraryCollection> collection = new List<LibraryCollection>();

            using (var context = new LibraryDBEntities())
            {
                foreach (var lib in context.Library)
                {
                    foreach (var bk in context.LibraryItem.Where(l => l.LibraryId == lib.Id))
                    {

                        int count = context.LibraryItem.Where(c => c.BookId == context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault().Id).FirstOrDefault().Count;

                        new LibraryCollection
                        {
                            Title = lib.Title
                        }.ListOfBooks.Add(new BookInLibrary { Book = context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault(), Count = count });
                    }

                    collection.Add(new LibraryCollection
                    {
                        Title = lib.Title
                    });
                }

                ViewBag.IsAdmin = context.User.Where(u => u.Id == userId).FirstOrDefault().IsAdmin;

                HttpContext.Response.Cookies["user"].Value = userId.ToString();

                ViewBag.UserName = context.User.Where(u => u.Id == userId).FirstOrDefault().Name;
            }

            return View(collection);
        }
    }
}