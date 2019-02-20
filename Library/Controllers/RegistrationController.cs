using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index()
        {

            return View("Registration");
        }

        public ActionResult Registrate(string name, string login, string email, string password)
        {
            using (var context = new LibraryDBEntities())
            {
                if(context.User.Any(u => u.Login == login))
                {
                    ViewBag.Message = "User with this login already exist";
                    return View("Registration");
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
            }
            return View("Success");
        }
    }
}