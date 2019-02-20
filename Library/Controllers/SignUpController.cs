using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class SignUpController : Controller
    {
        [Route("/SignUp")]
        public ActionResult Index()
        {
            HttpContext.Response.Cookies["user"].Value = "";

            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(string login, string password)
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
                    ViewBag.Message = "User not exist!";
                    return View("SignUp");
                }

                if (user != null && user.Password == password)
                {
                    HttpContext.Response.Cookies["user"].Value = user.Id.ToString();
                    return RedirectToAction("Index", "Home", new { userId = user.Id });
                }
                else
                {
                    ViewBag.Message = "Password is wrong!";
                    return View();
                }
            }
        }
    }
}