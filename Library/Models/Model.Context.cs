namespace Library.Models
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class LibraryDBEntities : DbContext
    {
        public LibraryDBEntities() : base("name=LibraryDBEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Library> Library { get; set; }
        public virtual DbSet<LibraryItem> LibraryItem { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}