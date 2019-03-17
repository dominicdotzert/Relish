using System;
using System.Threading.Tasks;

namespace Relish.Services

{
    public interface IRestService
    {
        /// <summary>
        /// Calls the firebase function and listens for the response.
        /// </summary>
        /// <returns>The recipe data.</returns>
        Task<string> GetRecipeAsync();
    }
}
