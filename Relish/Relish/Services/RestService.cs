using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Relish.Services
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.RestUrl)
            };
        }

        public async Task<string> GetRecipeAsync()
        {
            string content = null;

            HttpResponseMessage resp = await client.GetAsync(Constants.getSingleRecipe);

            try
            {
                if (resp.IsSuccessStatusCode)
                {
                    content = await resp.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"             ERROR {0}", e.Message);
            }

            return content;
        }
    }
}
