using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Relish.Models;
using Relish.Models.Filters;
using Xamarin.Forms;

namespace Relish.Database
{
    public class SearchQuery
    {
        private const string BaseUrl = @"https://us-central1-relish-4f211.cloudfunctions.net";
        private const string GetRecipesEndPoint = @"/dbAPI/recipes/getRecipe/filter?";

        private readonly List<Filter> _filterList;
        private readonly HttpClient _client;

        public SearchQuery(List<Filter> filterList)
        {
            _filterList = filterList;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<Recipe>> StartSearch()
        {
            ////var query = GetRecipesEndPoint;

            ////for (int i = 0; i < _filterList.Count; i++)
            ////{
            ////    if (i != 0)
            ////    {
            ////        query += "&";
            ////    }
            ////    query += _filterList[i].ReturnQueryElement();
            ////}

            ////var response = await _client.GetAsync(query);

            ////try
            ////{
            ////    var content = await response.Content.ReadAsStringAsync();
            ////}
            ////catch
            ////{
            ////    return null;
            ////}

            await Task.Delay(TimeSpan.FromSeconds(3));

            return DummySearchData.RecipeResults1;
        }
    }

    public static class DummySearchData
    {
        public static List<Recipe> RecipeResults1 =>
            new List<Recipe>()
            {
                new Recipe(
                    "Corned Beef and Cabbage",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/03/corned-beef-cabbage-vertical-d-1200-212x300.jpg",
                    string.Empty,
                    0,
                    180),
                new Recipe(
                    "Chicken Curry with Sweet Potato and Lemongrass",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2-214x300.jpg",
                    "4 to 6 servings",
                    5,
                    40),
                new Recipe(
                    "BBQ Chicken Burrito Bowl",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/07/2016-08-03-BBQ-Chicken-Bowls-9-200x300.jpg",
                    "4 servings",
                    15,
                    10),
                new Recipe(
                    "Easy No-Bean Chili",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                    "6 servings",
                    20,
                    30),
            };
    }
}
