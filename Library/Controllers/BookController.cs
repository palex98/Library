using Library.Models;
using Library.Models.Custom;
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
            return Book.GetBooks(id);
        }

        public void PostBook([FromBody]PostBookParams prms)
        {
            Book.CreateBook(prms);
        }

        public void DeleteBook([FromBody]DelBookParams bookId)
        {
            Book.DeleteBook(bookId);
        }   
    }
}