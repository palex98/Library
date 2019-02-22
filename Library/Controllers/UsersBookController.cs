using Library.Models.Custom;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class UsersBookController : ApiController
    {
        IBookUserRepository repo;

        public UsersBookController(IBookUserRepository r)
        {
            repo = r;
        }

        public void DeleteReturnBook([FromBody]ReturnParams prms)
        {
            repo.ReturnBook(prms);
        }

        public HttpResponseMessage PutBookIntoUser([FromBody]PutBookParams prms)
        {
            var message = repo.TakeBook(prms);

            if (message == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
        }
    }
}
