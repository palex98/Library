using System;
using System.Linq;

namespace Library.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int BookCount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UserRepository : IDisposable, IUserRepository
    {
        private LibraryDBEntities context = new LibraryDBEntities();

        public Info GetUserInfo(int id)
        {
            var user = context.User.Where(u => u.Id == id).FirstOrDefault();

            return new Info
            {
                Name = user.Name,
                isAdmin = user.IsAdmin
            };
        }

        public User GetUserForSignUp(string login)
        {
            User user;

            try
            {
                user = context.User.First(u => u.Login == login);
            }
            catch
            {
                user = null;
            }

            return user;
        }

        public bool Registration(string name, string login, string email, string password)
        {
            if (context.User.Any(u => u.Login == login))
            {
                return false;
            }

            context.User.Add(new User
            {
                Name = name,
                Login = login,
                Password = password,
                Email = email,
                BookCount = 0,
                IsAdmin = false
            });

            context.SaveChanges();

            return true;
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

    public interface IUserRepository
    {
        Info GetUserInfo(int id);
        User GetUserForSignUp(string login);
        bool Registration(string name, string login, string email, string password);
    }

    public class Info
    {
        public string Name { get; set; }
        public bool isAdmin { get; set; }
    }
}
