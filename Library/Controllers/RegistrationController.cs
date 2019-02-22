using Library.Models;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RegistrationController : Controller
    {
        IUserRepository repo;

        public RegistrationController(IUserRepository r)
        {
            repo = r;
        }

        public ActionResult Index()
        {
            return View("Registration");
        }

        public ActionResult Registrate(string name, string login, string email, string password)
        {
            if (repo.Registration(name, login, email, password))
            {
                return View("Success");
            }
            else
            {
                ViewBag.Message = "User with this login already exist";
                return View("Registration");
            }
        }
    }
}