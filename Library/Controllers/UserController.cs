using Library.Models;
using System.Web.Http;

namespace Library.Controllers
{
    public class UserController : ApiController
    {
        public Info GetUserInfo(int id)
        {
            return Models.User.GetUserInfo(id);
        }
    }
}
