using Library.Models.Custom;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class UsersBookController : ApiController
    {
        public void DeleteReturnBook([FromBody]ReturnParams prms)
        {
            BookUser.ReturnBook(prms);
        }

        public HttpResponseMessage PutBookIntoUser([FromBody]PutBookParams prms)
        {
            var message = BookUser.TakeBook(prms);

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
