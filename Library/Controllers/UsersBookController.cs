using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class UsersBookController : ApiController
    {
        public void DeleteReturnBook([FromBody]DeleteParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                var loan = context.Loan.Where(l => l.BookId == prms.bookId && l.UserId == prms.userId).FirstOrDefault();

                context.Loan.Remove(loan);

                context.SaveChanges();

                try
                {
                    var item = context.LibraryItem.Where(i => i.BookId == prms.bookId).First();
                    item.Count++;
                    context.SaveChanges();
                }
                finally
                {
                    //DO NOTHING
                }
            }
        }

        public HttpResponseMessage PutBookToUser([FromBody]PutParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                var loans = context.Loan.Where(l => l.UserId == prms.userId);

                if (loans.Count() > 2)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You have reached the limit! Return any book to take a new.");
                }

                if (loans.Any(l => l.BookId == prms.bookId))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You already have this book!");

                }

                var libraryItem = context.LibraryItem.Where(l => l.BookId == prms.bookId).FirstOrDefault();

                Loan loan = new Loan
                {
                    UserId = prms.userId,
                    LibraryId = libraryItem.LibraryId,
                    BookId = prms.bookId,
                    EndDate = prms.date
                };

                libraryItem.Count--;

                context.Loan.Add(loan);

                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class DeleteParams
    {
        public int bookId { get; set; }
        public int userId { get; set; }
    }

    public class PutParams
    {
        public int userId { get; set; }
        public int bookId { get; set; }
        public DateTime date { get; set; }
    }
}
