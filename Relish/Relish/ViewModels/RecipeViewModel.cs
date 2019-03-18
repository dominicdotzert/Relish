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
    }
}
