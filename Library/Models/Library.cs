using Library.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Models
{
    public partial class Library
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class LibraryRepository : IDisposable, ILibraryRepository
    {
        private LibraryDBEntities context = new LibraryDBEntities();

        public List<LibraryCollection> GetLibraries(int sort)
        {
            List<LibraryCollection> collection = new List<LibraryCollection>();

            foreach (var lib in context.Library)
            {
                LibraryCollection libraryCollection = new LibraryCollection
                {
                    Title = lib.Title
                };

                foreach (var bk in context.LibraryItem.Where(l => l.LibraryId == lib.Id))
                {
                    Book book = context.Book.Where(b => b.Id == bk.BookId).FirstOrDefault();

                    int count = context.LibraryItem.Where(c => c.BookId == book.Id).FirstOrDefault().Count;

                    BookInLibrary bookInLibrary = new BookInLibrary { Book = book, Count = count };

                    libraryCollection.ListOfBooks.Add(bookInLibrary);
                }
                collection.Add(libraryCollection);
            }

            SortLibraryCollection(collection, sort);

            return collection;
        }

        public void CreateLibrary(TitleParam title)
        {
            Library newLibrary = new Library
            {
                Title = title.Title
            };

            context.Library.Add(newLibrary);

            context.SaveChanges();
        }

        public void DeleteLibrary(TitleParam title)
        {
            var library = context.Library.Where(l => l.Title == title.Title).FirstOrDefault();

            var items = context.LibraryItem.Where(i => i.LibraryId == library.Id);

            context.LibraryItem.RemoveRange(items);

            var loans = context.Loan.Where(l => l.LibraryId == library.Id);

            context.Loan.RemoveRange(loans);

            context.Library.Remove(library);

            context.SaveChanges();
        }

        public void SortLibraryCollection(List<LibraryCollection> collection, int sort)
        {
            if (sort == 1)
            {
                foreach (var c in collection)
                {
                    c.ListOfBooks = c.ListOfBooks.OrderBy(b => b.Book.Author).ToList();
                }
            }
            else
            {
                foreach (var c in collection)
                {
                    c.ListOfBooks = c.ListOfBooks.OrderBy(b => b.Book.Genre).ToList();
                }
            }
        }

        public void ChangeCounter(PutCounterParams prms)
        {
            var book = context.LibraryItem.Where(i => i.BookId == prms.bookId).FirstOrDefault();

            book.Count = prms.value;

            context.SaveChanges();
        }

        public List<Library> GetList()
        {
            using (var context = new LibraryDBEntities())
            {
                var libraries = context.Library.ToList();

                return libraries;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface ILibraryRepository
    {
        List<LibraryCollection> GetLibraries(int sort);
        void CreateLibrary(TitleParam title);
        void DeleteLibrary(TitleParam title);
        void SortLibraryCollection(List<LibraryCollection> collection, int sort);
        void ChangeCounter(PutCounterParams prms);
        List<Models.Library> GetList();
    }

    public class TitleParam
    {
        public string Title { get; set; }
    }

    public class PutCounterParams
    {
        public int value { get; set; }
        public int bookId { get; set; }
    }
}
