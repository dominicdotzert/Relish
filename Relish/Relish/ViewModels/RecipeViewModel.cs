using System.Collections.Generic;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    public class RecipeViewModel : NotifyPropertyChanged
    {
        private readonly Recipe _recipe;
        private readonly LocalDataManager _localDataManager;

        public RecipeViewModel(Recipe recipe, LocalDataManager localDataManager)
        {
            _recipe = recipe;
            _localDataManager = localDataManager;

            SaveCommand = new Command(ToggleSaved);
            PrepareCommand = new Command(TogglePrepared);
        }

        public string Name => _recipe.Name;

        public string ThumbnailUrl => _recipe.ThumbnailUrl;

        public string ImageUrl => _recipe.ImageUrl;

        public string Url => _recipe.Url;

        public string PrepTime => "Prep Time: " + (_recipe.PrepTime == 0 ? string.Empty : _recipe.PrepTime.ToString());

        public string CookTime => "Cook Time: " + (_recipe.CookTime == 0 ? string.Empty : _recipe.CookTime.ToString());

        public string Servings => _recipe.ServingSize;

        public string Cuisine => $"Cuisine: {_recipe.Cuisine}";

        public string PrepStyle => $"Prep Time: {_recipe.PrepStyle}";

        public string MealType => $"Meal Type: {_recipe.MealType}";

        public List<string> Ingredients => _recipe.Ingredients;

        public List<string> Directions => _recipe.Directions;

        public bool Saved
        {
            get => _recipe.IsSaved;

            set
            {
                if (_recipe.IsSaved != value)
                {
                    _recipe.IsSaved = value;
                    OnPropertyChanged(nameof(Saved));
                    OnPropertyChanged(nameof(SaveButtonText));
                }
            }
        }

        public string SaveButtonText => !Saved ? "Save" : "Unsave";

        public bool Prepared
        {
            get => _recipe.IsMealPrepped;

            set
            {
                if (_recipe.IsMealPrepped != value)
                {
                    _recipe.IsMealPrepped = value;
                    OnPropertyChanged(nameof(Prepared));
                    OnPropertyChanged(nameof(PrepareButtonText));
                }
            }
        }

        public string PrepareButtonText => !Prepared ? "Prepare" : "Unprepare";

        public ICommand SaveCommand { get; }

        public ICommand PrepareCommand { get; }

        private void ToggleSaved()
        {
            Saved = !Saved;

            _localDataManager.UpdateRecipe(_recipe);
        }

        private async void TogglePrepared()
        {
            Prepared = !Prepared;

            await _localDataManager.UpdateRecipe(_recipe);
            var result = await _localDataManager.GetRecipes();
        }
    }
}
