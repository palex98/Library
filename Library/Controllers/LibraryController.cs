using Library.Models;
using Library.Models.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class LibraryController : ApiController
    {

        public List<LibraryCollection> GetLibraries(int sort)
        {
            return Models.Library.GetLibraries(sort);
        }

        public void PostLibrary([FromBody]TitleParam title)
        {
            Models.Library.CreateLibrary(title);
        }

        public void DeleteLibrary([FromBody]TitleParam title)
        {
            Models.Library.DeleteLibrary(title);
        }
    }


}
