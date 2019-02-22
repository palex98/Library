using Library.Models;
using Library.Models.Custom;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
{
    public class LibraryController : ApiController
    {
        ILibraryRepository repo;

        public LibraryController(ILibraryRepository r)
        {
            repo = r;
        }

        public List<LibraryCollection> GetLibraries(int sort)
        {
            return repo.GetLibraries(sort);
        }

        public void PostLibrary([FromBody]TitleParam title)
        {
            repo.CreateLibrary(title);
        }

        public void DeleteLibrary([FromBody]TitleParam title)
        {
            repo.DeleteLibrary(title);
        }
    }
}
