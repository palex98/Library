using Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
{
    public class CounterController : ApiController
    {
        ILibraryRepository repo;

        public CounterController(ILibraryRepository r)
        {
            repo = r;
        }

        public List<Models.Library> GetListOfLibraries()
        {
            return repo.GetList();
        }

        public void PutChangeCounter([FromBody]PutCounterParams prms)
        {
            repo.ChangeCounter(prms);
        }     
    }
}
