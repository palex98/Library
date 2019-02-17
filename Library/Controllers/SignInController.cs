using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class SignInController : Controller
    {
        // GET: SingIn
        public ActionResult Index()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(string login, string password)
        {
            using (var context = new LibraryDBEntities())
            {
                User user;
            
                try
                {
                    user = context.User.First(u => u.Login == login);
                }
                catch (Exception)
                {
                    return View("SignIn");
                }

                if (user != null && user.Password == password)
                {
                    return RedirectToAction("Index", "Home", new { userId = user.Id });
                }
                return View();
            }
        }
    }
}