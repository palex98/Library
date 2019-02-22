namespace Library.Models
{   
    public partial class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
