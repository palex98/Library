using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class UserController : ApiController
    {
        public Info GetUserInfo(int id)
        {
            using (var context = new LibraryDBEntities())
            {
                var user = context.User.Where(u => u.Id == id).FirstOrDefault();

                Info info = new Info
                {
                    Name = user.Name,
                    isAdmin = user.IsAdmin
                };

                return info;
            }
        }
    }

    public class Info
    {
        public string Name { get; set; }
        public bool isAdmin { get; set; }
    }
}
