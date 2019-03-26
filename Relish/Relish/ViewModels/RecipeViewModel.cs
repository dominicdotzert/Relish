using System;
using System.Collections.Generic;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Relish.Resources;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    /// <summary>
    /// ViewModel class to represent the recipe data presented in the RecipeView page.
    /// </summary>
    public class RecipeViewModel : NotifyPropertyChanged
    {
        private readonly Recipe _recipe;
        private readonly LocalDataManager _localDataManager;

        /// <summary>
        /// Initializes the ViewModel object.
        /// </summary>
        /// <param name="recipe">The recipe to be displayed.</param>
        /// <param name="localDataManager">The LocalDataManager object for marking recipes as saved.</param>
        public RecipeViewModel(Recipe recipe, LocalDataManager localDataManager)
        {
            _recipe = recipe;
            _localDataManager = localDataManager;

            OpenInBrowserCommand = new Command(OpenInBrowser);
            SaveCommand = new Command(ToggleSaved);
            PrepareCommand = new Command(OpenPremiumPopup);

            CheckIngredients();
        }

        /// <summary>
        /// Event for when a recipe is removed from the Recipe Book.
        /// Will be used to refresh the Recipe Book list.
        /// </summary>
        public event EventHandler RecipeUnsaved;

        /// <summary>
        /// The string representing the Name of the recipe.
        /// </summary>
        public string Name => _recipe.Name;

        /// <summary>
        /// The string representing the url of the thumbnail image.
        /// </summary>
        public string ThumbnailUrl => _recipe.ThumbnailUrl;

        /// <summary>
        /// The string representing the url of the main recipe image.
        /// </summary>
        public string ImageUrl => _recipe.ImageUrl;

        /// <summary>
        /// The string representing the source url of the recipe.
        /// </summary>
        public string Url => _recipe.Url;

        /// <summary>
        /// The string representing the total preparation time for the recipe.
        /// </summary>
        public string PrepTime => _recipe.PrepTime == 0 ? string.Empty : _recipe.PrepTime.ToString() + $" {Strings.Minutes}";

        /// <summary>
        /// The string representing the total cooking time for the recipe.
        /// </summary>
        public string CookTime => _recipe.CookTime == 0 ? string.Empty : _recipe.CookTime.ToString() + $" {Strings.Minutes}";

        /// <summary>
        /// The string representing how many servings the recipe makes.
        /// </summary>
        public string Servings => _recipe.ServingSize;

        /// <summary>
        /// The string representing the type of cuisine of the recipe.
        /// </summary>
        public string Cuisine => _recipe.Cuisine;

        /// <summary>
        /// The string representing the preparation style of the recipe.
        /// </summary>
        public string PrepStyle => _recipe.PrepStyle;

        /// <summary>
        /// The string representing the type of meal of the recipe
        /// </summary>
        public string MealType => _recipe.MealType;

        /// <summary>
        /// The list of strings representing the ingredients required to make the recipe.
        /// </summary>
        public List<ReadonlyIngredient> Ingredients => _recipe.Ingredients;

        /// <summary>
        /// The list of strings representing the steps required to prepare the recipe.
        /// </summary>
        public List<string> Directions => _recipe.Directions;

        /// <summary>
        /// The flag representing if the user has saved the recipe locally to their device.
        /// </summary>
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

        /// <summary>
        /// The string representing the text displayed on the button for saving/unsaving the recipe.
        /// </summary>
        public string SaveButtonText => !Saved ?
            Strings.RecipeView_AddToRecipeBook : Strings.RecipeView_RemoveFromRecipeBook;

        /// <summary>
        /// The string representing the text displayed on the button for preparing/unpreparing the recipe from the meal prep list.
        /// </summary>
        public string PrepareButtonText => Strings.RecipeView_AddToMealPrep;

        /// <summary>
        /// Command for opening the original recipe in a browser.
        /// </summary>
        public ICommand OpenInBrowserCommand { get; }

        /// <summary>
        /// Command for saving the recipe locally to the device
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Command for adding/removing the recipe to the meal prep list.
        /// </summary>
        public ICommand PrepareCommand { get; }

        /// <summary>
        /// Opens the recipe source url in the default device browser.
        /// </summary>
        private void OpenInBrowser()
        {
            if (_recipe.Url != null)
            {
                Device.OpenUri(new Uri(_recipe.Url));
            }
        }

        /// <summary>
        /// Saves/removes the recipe to/from the device.
        /// </summary>
        private void ToggleSaved()
        {
            Saved = !Saved;

            _localDataManager.UpdateRecipe(_recipe);
            RecipeUnsaved?.Invoke(_recipe, null);
        }

        /// <summary>
        /// Opens the UpgradeToPremium popup when attempting to use the Meal Prep feature.
        /// </summary>
        private async void OpenPremiumPopup()
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count == 0 || stack[stack.Count - 1].GetType() != typeof(UpgradeToPremiumPopup))
            {
                await PopupNavigation.Instance.PushAsync(new UpgradeToPremiumPopup());
            }
        }

        /// <summary>
        /// Checks if the user has each recipe ingredient.
        /// </summary>
        private void CheckIngredients()
        {
            var userIngredients = _localDataManager.GetIngredients().Result;
            foreach (var recipeIngredient in Ingredients)
            {
                foreach (var userIngredient in userIngredients)
                {
                    if (recipeIngredient.Name.ToLower().Contains(userIngredient.Name.ToLower()) &&
                        (string.Equals(recipeIngredient.Unit, userIngredient.StandardUnit.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                         userIngredient.QuantityStandardUnit >= recipeIngredient.Quantity ||
                         userIngredient.Unit == Enums.Units.Common))
                    {
                        recipeIngredient.UserHasIngredient = true;
                        break;
                    }
                }
            }
        }
    }
}
