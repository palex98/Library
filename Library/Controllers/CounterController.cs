using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class CounterController : ApiController
    {

        public void PutChangeCounter([FromBody]PutCounterParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                var book = context.LibraryItem.Where(i => i.BookId == prms.bookId).FirstOrDefault();

                book.Count = prms.value;

                context.SaveChanges();
            }
        }
    }

    public class PutCounterParams
    {
        public int value { get; set; }
        public int bookId { get; set; }
    }
}
