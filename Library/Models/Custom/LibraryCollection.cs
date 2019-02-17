using System.Collections.Generic;

namespace Library.Models.Custom
{
    public class LibraryCollection
    {
        public string Title { get; set; }
        public List<BookInLibrary> ListOfBooks { get; set; }

        public LibraryCollection()
        {
            ListOfBooks = new List<BookInLibrary>();
        }
    }
}