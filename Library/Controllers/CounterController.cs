using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Library.Controllers
{
    public class CounterController : ApiController
    {
        public List<Models.Library> GetLibraries()
        {
            using (var context = new LibraryDBEntities())
            {
                var libraries = context.Library.ToList();

                return libraries;
            }
        }

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
