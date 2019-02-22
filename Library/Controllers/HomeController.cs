using Library.Models;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        ILibraryRepository libRepo;
        IUserRepository uRepo;

        public HomeController(ILibraryRepository lib, IUserRepository u)
        {
            libRepo = lib;
            uRepo = u;
        }

        [HttpGet]
        public ActionResult Index(int userId)
        {
            var userFromCookies = HttpContext.Request.Cookies["user"].Value;

            if (userFromCookies != userId.ToString())
            {
                return Redirect("/");
            }

            var user = uRepo.GetUserInfo(userId);

            ViewBag.IsAdmin = user.isAdmin;

            ViewBag.UserName = user.Name;

            HttpContext.Response.Cookies["user"].Value = userId.ToString();

            return View(libRepo.GetLibraries(1));
        }
    }
}