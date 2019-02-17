using Library.Models;
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
        [HttpPost]
        public HttpResponseMessage PostBook([FromBody]Params prms)
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

                //new

                context.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class Params
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Library { get; set; }
        public string Count { get; set; }
    }
}
//string title, string author, string genre, string library, string count