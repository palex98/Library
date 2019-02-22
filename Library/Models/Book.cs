using Library.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }

    public class BookRepository : IDisposable, IBookRepository
    {
        public LibraryDBEntities context = new LibraryDBEntities();

        public List<BookUser> GetBooks(int id)
        {
            var loans = context.Loan.Where(u => u.UserId == id);

            List<BookUser> books = new List<BookUser>();

            foreach (var l in loans)
            {
                Book book = context.Book.Where(b => b.Id == l.BookId).FirstOrDefault();

                BookUser item = new BookUser
                {
                    Book = book,
                    EndDate = l.EndDate.ToString()
                };

                books.Add(item);
            }
            return books;
        }

        public void CreateBook(PostBookParams prms)
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

        public void DeleteBook(DelBookParams bookId)
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

    public interface IBookRepository
    {
        List<BookUser> GetBooks(int id);
        void CreateBook(PostBookParams prms);
        void DeleteBook(DelBookParams bookId);
    }

    public class DelBookParams
    {
        public int bookId { get; set; }
    }

    public class PostBookParams
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string LibraryTitle { get; set; }
        public string Count { get; set; }
    }
}
