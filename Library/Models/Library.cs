using Library.Models.Custom;
using System.Collections.Generic;
using System.Linq;

namespace Library.Models
{
    public partial class Library
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static List<LibraryCollection> GetLibraries(int sort)
        {
            List<LibraryCollection> collection = new List<LibraryCollection>();

            using (var context = new LibraryDBEntities())
            {

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
            }
            return collection;
        }

        public static void CreateLibrary(TitleParam title)
        {
            using (var context = new LibraryDBEntities())
            {
                Library newLibrary = new Library
                {
                    Title = title.Title
                };

                context.Library.Add(newLibrary);

                context.SaveChanges();
            }
        }

        public static void DeleteLibrary(TitleParam title)
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

        public static void SortLibraryCollection(List<LibraryCollection> collection, int sort)
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
    }

    public class TitleParam
    {
        public string Title { get; set; }
    }
}
