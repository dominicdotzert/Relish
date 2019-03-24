using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relish.Models;

namespace Relish.Data
{
    /// <summary>
    /// Static parser class to parse the HTTP response from JSON
    /// </summary>
    public static class JsonParser
    {
        private const string RecipeInfoKey = "recipeInfo";
        private const string IngredientsKey = "ingredients";
        private const string IngredientsListKey = "ingredientList";
        private const string NameKey = "name";
        private const string ThumbnailKey = "thumbnail";
        private const string ImageUrlKey = "image";
        private const string UrlKey = "recipeURL";
        private const string ServingSizeKey = "servingSize";
        private const string PrepTimeKey = "prepTime";
        private const string CookTimKey = "cookTime";
        private const string CuisineKey = "cuisineType";
        private const string PrepStyleKey = "prepStyle";
        private const string MealTypeKey = "course";
        private const string DirectionsKey = "directions";
        private const string IngredientNameKey = "name";
        private const string UnitKey = "unit";
        private const string QuantityKey = "amount";
        private const string IngredientFullNameKey = "fullData";

        public static List<Recipe> ParseJson(string content)
        {
            var recipes = new List<Recipe>();

            var json = JArray.Parse(content);

            foreach (var recipe in json)
            {
                var name = recipe[NameKey].ToString();
                var url = recipe[UrlKey].ToString();
                var thumbnail = recipe[ThumbnailKey]?.ToString();
                var image = recipe[ImageUrlKey]?.ToString();
                var servings = recipe[ServingSizeKey]?.ToString();
                var prepTime = recipe[PrepTimeKey]?.ToObject<int>();
                var cookTime = recipe[CookTimKey]?.ToObject<int>();

                var cuisineList = recipe[CuisineKey]?.ToObject<List<string>>();
                var cuisine = cuisineList == null ? string.Empty : string.Join(", ", cuisineList);
                var prepStyleList = recipe[PrepStyleKey]?.ToObject<List<string>>();
                var prepStyle = prepStyleList == null ? string.Empty : string.Join(", ", prepStyleList);
                var mealTypeList = recipe[MealTypeKey]?.ToObject<List<string>>();
                var mealType = mealTypeList == null ? string.Empty : string.Join(", ", mealTypeList);

                var ingredients = new List<string>();
                var ingredientsList = recipe[IngredientsKey];
                foreach (var i in ingredientsList)
                {
                    ingredients.Add(i[IngredientNameKey].ToString());
                }

                var directions = recipe[DirectionsKey].ToObject<List<string>>();

                recipes.Add(new Recipe(
                    name,
                    thumbnail,
                    image,
                    url,
                    servings,
                    prepTime ?? 0,
                    cookTime ?? 0,
                    cuisine,
                    prepStyle,
                    mealType,
                    ingredients,
                    directions));
            }

            return recipes;
        }

        public static List<Recipe> ParseJsonOld(string content)
        {
            var recipes = new List<Recipe>();

            #region Loop here

            // TODO convert to JArray when possible
            var json = JArray.Parse(content);

            foreach (var recipe in json)
            {
                var recipeInfo = recipe[RecipeInfoKey];
                var ingredientsRoot = recipe[IngredientsKey];

                var name = recipeInfo[NameKey].ToString();
                var url = recipeInfo[UrlKey].ToString();
                var thumbnail = recipeInfo[ThumbnailKey]?.ToString();
                var image = recipeInfo[ImageUrlKey]?.ToString();
                var servings = recipeInfo[ServingSizeKey]?.ToString();
                var prepTime = recipeInfo[PrepTimeKey]?.ToObject<int>();
                var cookTime = recipeInfo[CookTimKey]?.ToObject<int>();

                var cuisineList = recipeInfo[CuisineKey]?.ToObject<List<string>>();
                var cuisine = cuisineList == null ? string.Empty : string.Join(", ", cuisineList);
                var prepStyleList = recipeInfo[PrepStyleKey]?.ToObject<List<string>>();
                var prepStyle = prepStyleList == null ? string.Empty : string.Join(", ", prepStyleList);
                var mealTypeList = recipeInfo[MealTypeKey]?.ToObject<List<string>>();
                var mealType = mealTypeList == null ? string.Empty : string.Join(", ", mealTypeList);

                var directions = recipeInfo[DirectionsKey].Select(x => x.ToString()).ToList();

                var ingredients = new List<string>();
                foreach (var i in ingredientsRoot)
                {
                    var ingredientName = i[IngredientNameKey].ToString();
                    var quantity = i[QuantityKey].ToString();
                    var unit = i[UnitKey].ToString();

                    ingredients.Add($"{ingredientName}: {quantity} ({unit})");
                }

                recipes.Add(new Recipe(
                    name,
                    thumbnail,
                    image,
                    url,
                    servings,
                    prepTime ?? 0,
                    cookTime ?? 0,
                    cuisine,
                    prepStyle,
                    mealType,
                    ingredients,
                    directions));
            }

            #endregion

            return recipes;
        }
    }
}
