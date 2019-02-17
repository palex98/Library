using Library.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class BookController : ApiController
    {

        public HttpResponseMessage PostBook([FromBody]BookParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                Book newBook = new Book
                {
                    Title = prms.Title,
                    Author = prms.Author,
                    Genre = prms.Genre
                };

                var book = context.Book.Add(newBook);

                context.SaveChanges();

                int available;

                try
                {
                    int.TryParse(prms.Count, out available);

                    if (available < 0)
                    {
                        available = 0;
                    }
                }
                catch
                {
                    available = 0;
                }

                LibraryItem newItem = new LibraryItem
                {
                    BookId = book.Id,
                    LibraryId = context.Library.Where(i => i.Title == prms.LibraryTitle).FirstOrDefault().Id,
                    Count = available
                };

                context.LibraryItem.Add(newItem);

                context.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DeleteBook([FromBody]Del bookId)
        {

            int.TryParse(bookId.bookId, out int id);

            using (var context = new LibraryDBEntities())
            {
                var bookToDelete = context.Book.Where(b => b.Id == id).FirstOrDefault();

                var itemInLibraryToDelete = context.LibraryItem.Where(i => i.BookId == id).FirstOrDefault();

                context.Book.Remove(bookToDelete);
                context.LibraryItem.Remove(itemInLibraryToDelete);

                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class Del
    {
        public string bookId { get; set; }
    }

    public class BookParams
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string LibraryTitle { get; set; }
        public string Count { get; set; }
    }
}