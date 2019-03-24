using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Relish.Models;
using Relish.Models.Filters;

namespace Relish.Data
{
    public class SearchQuery
    {
        private const string BaseUrl = @"https://us-central1-relish-4f211.cloudfunctions.net";
        private const string GetRecipesEndPoint = @"/recipeHandlerAPI/recipes/getRecipe/filter?";
        private const string TestEndPoint = @"/recipeHandlerAPI/recipes/getRecipe?id=uhuowqZGz4oJoih0pgco";

        private readonly List<Filter> _filterList;
        private readonly LocalDataManager _localDataManager;
        private readonly HttpClient _client;

        public SearchQuery(List<Filter> filterList, LocalDataManager localDataManager)
        {
            _filterList = filterList;
            _localDataManager = localDataManager;
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<Recipe>> StartSearch()
        {
            // TODO remove fake list
            ////await Task.Delay(TimeSpan.FromSeconds(1));
            ////return DummySearchData.RecipeResults1;

            // Get query
            var query = FormQuery();

            // Hit endpoint and await response
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(query);
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }

            // Extract response
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
            {
                return new List<Recipe>(); // return empty list
            }

            // Parse response
            var recipes = JsonParser.ParseJson(content);

            if (recipes.Count == 0)
            {
                return recipes;
            }

            // Check if any recipes were previously saved and update recipe flags
            var savedRecipes = await _localDataManager.GetRecipes();
            foreach (var r in recipes)
            {
                foreach (var saved in savedRecipes)
                {
                    if (r.Url == saved.Url)
                    {
                        r.IsSaved = saved.IsSaved;
                    }
                }
            }

            return recipes;
        }

        private string FormQuery()
        {
            // TODO implement query when database is ready
            var query = GetRecipesEndPoint;
            var paramsList = new List<string>();

            foreach (var filter in _filterList)
            {
                paramsList.Add(filter.ReturnQueryElement());
            }

            query += string.Join("&", paramsList);

            return query;

            //For testing
            ////return TestEndPoint;
        }
    }

    public static class DummySearchData
    {
        public static List<Recipe> RecipeResults1 =>
            new List<Recipe>
            {
                new Recipe(
                    "Corned Beef and Cabbage",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/03/corned-beef-cabbage-vertical-d-1200-212x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/03/corned-beef-cabbage-vertical-d-1200-212x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/03/corned-beef-cabbage-vertical-d-1200-212x300.jpg",
                    string.Empty,
                    0,
                    180,
                    "Cuisine",
                    "PrepStyle",
                    "MealType",
                    new List<string> {"Ingredient 1", "Ingredient 2", "Ingredient 3", "Ingredient 4"},
                    new List<string> {"Direction 1", "Direction 2", "Direction 3", "Direction 4"}),
                new Recipe(
                    "Chicken Curry with Sweet Potato and Lemongrass",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2-214x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2-214x300.jpg",
                    "4 to 6 servings",
                    5,
                    40,
                    "Cuisine",
                    "PrepStyle",
                    "MealType",
                    new List<string>() {"Ingredient 1", "Ingredient 2", "Ingredient 3", "Ingredient 4"},
                    new List<string>() {"Direction 1", "Direction 2", "Direction 3", "Direction 4"}),
                new Recipe(
                    "BBQ Chicken Burrito Bowl",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/07/2016-08-03-BBQ-Chicken-Bowls-9-200x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/07/2016-08-03-BBQ-Chicken-Bowls-9-200x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2016/07/2016-08-03-BBQ-Chicken-Bowls-9-200x300.jpg",
                    "4 servings",
                    15,
                    10,
                    "Cuisine",
                    "PrepStyle",
                    "MealType",
                    new List<string>() {"Ingredient 1", "Ingredient 2", "Ingredient 3", "Ingredient 4"},
                    new List<string>() {"Direction 1", "Direction 2", "Direction 3", "Direction 4"}),
                new Recipe(
                    "Easy No-Bean Chili",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                    "6 servings",
                    20,
                    30,
                    "Cuisine",
                    "PrepStyle",
                    "MealType",
                    new List<string>() {"Ingredient 1", "Ingredient 2", "Ingredient 3", "Ingredient 4"},
                    new List<string>() {"Direction 1", "Direction 2", "Direction 3", "Direction 4"}),
            };
    }
}
