using Library.Models;
using Library.Models.Custom;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
{
    public class BookController : ApiController
    {
        IBookRepository repo;

        public BookController(IBookRepository r)
        {
            repo = r;
        }

        public List<BookUser> GetUsersBook(int id)
        {
            return repo.GetBooks(id);
        }

        public void PostBook([FromBody]PostBookParams prms)
        {
            repo.CreateBook(prms);
        }

        public void DeleteBook([FromBody]DelBookParams bookId)
        {
            repo.DeleteBook(bookId);
        }   
    }
}