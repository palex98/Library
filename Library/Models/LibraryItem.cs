namespace Library.Models
{  
    public partial class LibraryItem
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
    }
}
