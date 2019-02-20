namespace Library.Models
{
    using System.Linq;

    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int BookCount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public static Info GetUserInfo(int id)
        {
            using (var context = new LibraryDBEntities())
            {
                var user = context.User.Where(u => u.Id == id).FirstOrDefault();

                return new Info
                {
                    Name = user.Name,
                    isAdmin = user.IsAdmin
                };
            }
        }
    }

    public class Info
    {
        public string Name { get; set; }
        public bool isAdmin { get; set; }
    }
}
