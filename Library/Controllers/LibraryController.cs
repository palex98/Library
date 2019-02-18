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
    public class LibraryController : ApiController
    {

        public List<LibraryCollection> GetLibraries()
        {
            List<LibraryCollection> collection = new List<LibraryCollection>();

            using (var context = new LibraryDBEntities())
            {
                var libraries = context.Library;

                foreach (var lib in libraries)
                {

                    LibraryCollection libraryCollection = new LibraryCollection();

                    libraryCollection.Title = lib.Title;

                    var books = context.LibraryItem.Where(l => l.LibraryId == lib.Id);

                    foreach (var bk in books)
                    {
                        Book book = context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault();

                        int count = context.LibraryItem.Where(c => c.BookId == book.Id).FirstOrDefault().Count;

                        BookInLibrary bookInLibrary = new BookInLibrary { Book = book, Count = count };

                        libraryCollection.ListOfBooks.Add(bookInLibrary);
                    }

                    collection.Add(libraryCollection);
                }
            }
            return collection;
        }

        public HttpResponseMessage PostLibrary([FromBody]PostParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                Models.Library newLibrary = new Models.Library
                {
                    Title = prms.Title
                };

                context.Library.Add(newLibrary);

                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public void DeleteLibrary([FromBody]DeleteLParams title)
        {
            using (var context = new LibraryDBEntities())
            {
                var library = context.Library.Where(l => l.Title == title.Title).FirstOrDefault();

                var items = context.LibraryItem.Where(i => i.LibraryId == library.Id);

                context.LibraryItem.RemoveRange(items);

                var loans = context.Loan.Where(l => l.LibraryId == library.Id);

                context.Loan.RemoveRange(loans);

                context.Library.Remove(library);

                context.SaveChanges();
            }
        }
    }

    public class DeleteLParams
    {
        public string Title { get; set; }
    }

    public class PostParams
    {
        public string Title { get; set; }
    }
}
