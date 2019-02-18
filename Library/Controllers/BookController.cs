using Library.Models;
using Library.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class BookController : ApiController
    {
        public List<BookUser> GetUsersBook(int id)
        {
            using (var conetxt = new LibraryDBEntities())
            {
                var loans = conetxt.Loan.Where(u => u.UserId == id);

                List<BookUser> books = new List<BookUser>();

                foreach (var l in loans)
                {
                    Book book = conetxt.Book.Where(b => b.Id == l.BookId).FirstOrDefault();

                    BookUser item = new BookUser
                    {
                        Book = book,
                        EndDate = l.EndDate.ToString()
                    };

                    books.Add(item);
                }

                return books;
            }
        }

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

        public HttpResponseMessage DeleteBook([FromBody]DelParams bookId)
        {

            using (var context = new LibraryDBEntities())
            {
                var bookToDelete = context.Book.Where(b => b.Id == bookId.bookId).FirstOrDefault();

                var itemInLibraryToDelete = context.LibraryItem.Where(i => i.BookId == bookId.bookId).FirstOrDefault();

                context.Book.Remove(bookToDelete);
                context.LibraryItem.Remove(itemInLibraryToDelete);

                var loans = context.Loan.Where(l => l.BookId == bookId.bookId);

                context.Loan.RemoveRange(loans);
                context.SaveChanges();

                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
    }



    public class DelParams
    {
        public int bookId { get; set; }
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