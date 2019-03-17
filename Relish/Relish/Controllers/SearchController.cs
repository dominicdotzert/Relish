using System.Threading.Tasks;
using Relish.Services;

namespace Relish.Controllers
{
    public class SearchController : ISearchController
    {
        RestService restService;


        public SearchController()
        {
            restService = new RestService();
        }

        public void SearchControllerInit(RestService service)
        {
            restService = service;
        }

        public async Task<string> GetRecipeAsync()
        {
            var response = await restService.GetRecipeAsync();

            return response;
        }
    }
}
