using Library.Models;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class SignUpController : Controller
    {
        IUserRepository repo;

        public SignUpController(IUserRepository r)
        {
            repo = r;
        }

        [Route("/SignUp")]
        public ActionResult Index()
        {
            HttpContext.Response.Cookies["user"].Value = "";

            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(string login, string password)
        {
            var user = repo.GetUserForSignUp(login);

            if (user != null && user.Password == password)
            {
                HttpContext.Response.Cookies["user"].Value = user.Id.ToString();
                return RedirectToAction("Index", "Home", new { userId = user.Id });
            }
            else if (user == null)
            {
                ViewBag.Message = "User not exist!";
                return View("SignUp");
            }
            else
            {
                ViewBag.Message = "Password is wrong!";
                return View();
            }
        }
    }
}