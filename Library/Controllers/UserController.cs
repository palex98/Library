using Library.Models;
using System.Web.Http;

namespace Library.Controllers
{
    public class UserController : ApiController
    {
        IUserRepository repo;

        public UserController(IUserRepository r)
        {
            repo = r;
        }

        public Info GetUserInfo(int id)
        {
            return repo.GetUserInfo(id);
        }
    }
}
