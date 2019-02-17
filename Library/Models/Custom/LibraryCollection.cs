using System.Collections.Generic;

namespace Library.Models.Custom
{
    public class LibraryCollection
    {
        public int Number { get; set; }
        public List<BookInLibrary> ListOfBooks { get; set; }

        public LibraryCollection()
        {
            ListOfBooks = new List<BookInLibrary>();
        }
    }
}