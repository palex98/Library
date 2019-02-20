using System;
using System.Linq;

namespace Library.Models.Custom
{
    public class BookUser
    {
        public Book Book { get; set; }
        public string EndDate { get; set; }

        public static void ReturnBook(ReturnParams prms)
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
                catch
                {
                    //DO NOTHING
                }
            }
        }

        public static string TakeBook(PutBookParams prms)
        {
            using (var context = new LibraryDBEntities())
            {
                var loans = context.Loan.Where(l => l.UserId == prms.userId);

                if (loans.Count() > 2)
                {
                    return "You have reached the limit! Return any book to take a new.";
                }

                if (loans.Any(l => l.BookId == prms.bookId))
                {
                    return "You already have this book!";
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

            return null;
        }
    }

    public class ReturnParams
    {
        public int bookId { get; set; }
        public int userId { get; set; }
    }

    public class PutBookParams
    {
        public int userId { get; set; }
        public int bookId { get; set; }
        public DateTime date { get; set; }
    }
}