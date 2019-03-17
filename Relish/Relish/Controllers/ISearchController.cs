using System;
using Relish.Services;
using System.Threading.Tasks;

namespace Relish.Controllers
{
    public interface ISearchController
    {
        /// <summary>
        /// Initializes the seach controller.
        /// </summary>
        /// <param name="service">RestService</param>
        void SearchControllerInit(RestService service);

        /// <summary>
        /// Gets a single recipe by making an HTTP GET request.
        /// </summary>
        /// <returns>The recipe information from the db.</returns>
        Task<string> GetRecipeAsync();
    }
}
