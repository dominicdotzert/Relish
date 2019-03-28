using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Relish.Models;
using Relish.Models.Filters;

namespace Relish.Data
{
    /// <summary>
    /// Class to represent a search query to the Relish database.
    /// </summary>
    public class SearchQuery
    {
        private const string BaseUrl = @"https://us-central1-relish-4f211.cloudfunctions.net";
        private const string GetRecipesEndPoint = @"/recipeHandlerAPI/recipes/getRecipe/filter?";

        private readonly List<Filter> _filterList;
        private readonly LocalDataManager _localDataManager;
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a search query object.
        /// </summary>
        /// <param name="filterList">The list of filters to be applied.</param>
        /// <param name="localDataManager">The LocalDataManager object.</param>
        public SearchQuery(List<Filter> filterList, LocalDataManager localDataManager)
        {
            _filterList = filterList;
            _localDataManager = localDataManager;
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        /// <summary>
        /// Task to begin the search.
        /// </summary>
        /// <returns>Returns a list of recipes.</returns>
        public async Task<List<Recipe>> StartSearch()
        {
            // Get query
            var query = FormQuery();

            // Hit endpoint and await response.
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

            // Extract response.
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
            {
                return new List<Recipe>(); // return empty list
            }

            // Parse response.
            var recipes = JsonParser.ParseJson(content);

            if (recipes.Count == 0)
            {
                return recipes;
            }

            // Check if any recipes were previously saved and update recipe flags.
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

        /// <summary>
        /// Gets the query based on the filter list.
        /// </summary>
        /// <returns>The http GET request string.</returns>
        private string FormQuery()
        {
            var query = GetRecipesEndPoint;
            var paramsList = new List<string>();

            foreach (var filter in _filterList)
            {
                paramsList.Add(filter.ReturnQueryElement());
            }

            query += string.Join("&", paramsList);

            return query;
        }
    }

    public static class DummySearchData
    {
        public static ReadonlyIngredient SampleReadonlyIngredient = new ReadonlyIngredient
        {
            Name = "Ingredient",
            Unit = "Unit",
            Quantity = 4.0f,
            OriginalString = "1 units of ingredient"
        };

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
                    new List<ReadonlyIngredient>
                    {
                        new ReadonlyIngredient
                        {
                            Name = "Tomatoes",
                            Unit = "quantity",
                            Quantity = 5,
                            OriginalString = "5 tomatoes"
                        },
                        new ReadonlyIngredient
                        {
                            Name = "Olive oil",
                            Unit = "mL",
                            Quantity = 50f,
                            OriginalString = "2 tbsp olive oil"
                        },
                        new ReadonlyIngredient
                        {
                            Name = "Cabbage",
                            Unit = "g",
                            Quantity = 1000f,
                            OriginalString = "2 lbs cabbage"
                        },
                    },
                    new List<string> { "Direction 1", "Direction 2", "Direction 3", "Direction 4" }),
                ////new Recipe(
                ////    "Chicken Curry with Sweet Potato and Lemongrass",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2-214x300.jpg",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2.jpg",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/03/chicken_lemongrass_curry_HERO00001_V2-214x300.jpg",
                ////    "4 to 6 servings",
                ////    5,
                ////    40,
                ////    "Cuisine",
                ////    "PrepStyle",
                ////    "MealType",
                ////    new List<ReadonlyIngredient> { SampleReadonlyIngredient, SampleReadonlyIngredient, SampleReadonlyIngredient, SampleReadonlyIngredient },
                ////    new List<string>() { "Direction 1", "Direction 2", "Direction 3", "Direction 4" }),
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
                    new List<ReadonlyIngredient>
                    {
                        new ReadonlyIngredient
                        {
                            Name = "Chicken breast",
                            Unit = "quantity",
                            Quantity = 2,
                            OriginalString = "2 chicken breast"
                        },
                        new ReadonlyIngredient
                        {
                            Name = "Lettuce",
                            Unit = "quantity",
                            Quantity = 2f,
                            OriginalString = "Lots of lettuce"
                        },
                        new ReadonlyIngredient
                        {
                            Name = "BBQ sauce",
                            Unit = "Common",
                            Quantity = -1f,
                            OriginalString = "BBQ sauce"
                        },
                    },
                    new List<string>() { "Direction 1", "Direction 2", "Direction 3", "Direction 4" }),
                ////new Recipe(
                ////    "Easy No-Bean Chili",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                ////    "https://www.simplyrecipes.com/wp-content/uploads/2019/01/No-Bean-Chili-LEAD-2-214x300.jpg",
                ////    "6 servings",
                ////    20,
                ////    30,
                ////    "Cuisine",
                ////    "PrepStyle",
                ////    "MealType",
                ////    new List<ReadonlyIngredient> { SampleReadonlyIngredient, SampleReadonlyIngredient, SampleReadonlyIngredient, SampleReadonlyIngredient },
                ////    new List<string>() { "Direction 1", "Direction 2", "Direction 3", "Direction 4" }),
            };
    }
}
