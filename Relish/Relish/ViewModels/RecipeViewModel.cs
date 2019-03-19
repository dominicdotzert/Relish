using System.Collections.Generic;
using Relish.Models;

namespace Relish.ViewModels
{
    public class RecipeViewModel
    {
        private readonly Recipe _recipe;

        public RecipeViewModel(Recipe recipe)
        {
            _recipe = recipe;
        }

        public string Name => _recipe.Name;

        public string ThumbnailUrl => _recipe.ThumbnailUrl;

        public string ImageUrl => _recipe.ImageUrl;

        public string Url => _recipe.Url;

        public string PrepTime => "Prep Time: " + (_recipe.PrepTime == 0 ? string.Empty : _recipe.PrepTime.ToString());

        public string CookTime => "Cook Time: " + (_recipe.CookTime == 0 ? string.Empty : _recipe.CookTime.ToString());

        public string Servings => _recipe.ServingSize;

        ////public string Cuisine => "Cuisine: " + (string.IsNullOrEmpty(_recipe.Cuisine) ? string.Empty : _recipe.Cuisine);
        public string Cuisine => $"Cuisine: {_recipe.Cuisine}";

        ////public string PrepStyle => "Prep Time: " + (_recipe.PrepTime == 0 ? string.Empty : _recipe.PrepStyle);
        public string PrepStyle => $"Prep Time: {_recipe.PrepStyle}";

        public string MealType => $"Meal Type: {_recipe.MealType}";

        public List<string> Ingredients => _recipe.Ingredients;

        public List<string> Directions => _recipe.Directions;
    }
}
