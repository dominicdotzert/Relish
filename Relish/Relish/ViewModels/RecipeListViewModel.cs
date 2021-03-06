﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Relish.Utilities;
using Relish.Views;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    /// <summary>
    /// ViewModel class to load a list of recipes and display it to the RecipeListView page.
    /// </summary>
    public class RecipeListViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;
        private readonly INavigation _navigation;
        private readonly bool _containsSavedItems;

        private bool _loadError;
        private bool _searchComplete;
        private bool _searchHasResults;

        /// <summary>
        /// Initializes the RecipeListViewModel object.
        /// </summary>
        /// <param name="loadTask">The task to load the list of recipes to display.</param>
        /// <param name="localDataManager">The LocalDataManger object.</param>
        /// <param name="navigation">The INavigation object for managing pages.</param>
        /// <param name="titleString">The string to be displayed in the navigation bar.</param>
        /// <param name="noResultsString">The string to display if no recipes are returned.</param>
        /// <param name="containsSavedItems">Flag for whether the recipe list will contain saved recipe items.</param>
        public RecipeListViewModel(
            Task<List<Recipe>> loadTask,
            LocalDataManager localDataManager,
            INavigation navigation,
            string titleString,
            string noResultsString,
            bool containsSavedItems)
        {
            _localDataManager = localDataManager;
            _navigation = navigation;
            _containsSavedItems = containsSavedItems;

            TitleString = titleString;
            NoResultsString = noResultsString;
            OpenRecipeCommand = new Command(OpenRecipe);
            
            // Task to wait for recipe list to load, then show the result to the user.
            Task.Run(async () =>
            {
                try
                {
                    UserIngredients = await _localDataManager.GetIngredients();
                    var result = await loadTask;

                    if (_containsSavedItems)
                    {
                        CalculateMissingIngredients(result);
                        ////result.Sort(ObjectComparisons.SortByRecipeName);
                        result.Sort(ObjectComparisons.SortByMissingIngredients);
                    }

                    RecipeResults = new ObservableCollection<Recipe>(result);
                    SearchHasResults = RecipeResults.Count != 0;
                }
                catch (Exception e)
                {
                    LoadError = true;
                    SearchHasResults = true; // Set to true in order to hide NoResults Label.
                    RecipeResults = new ObservableCollection<Recipe>();
                }

                SearchComplete = true;
                OnPropertyChanged(nameof(RecipeResults));
            });
        }

        /// <summary>
        /// Command to open the RecipeView page when the user selects a recipe.
        /// </summary>
        public ICommand OpenRecipeCommand { get; }

        /// <summary>
        /// Flag which represents if a load error has occurred.
        /// If true, a generic error string will be displayed.
        /// </summary>
        public bool LoadError
        {
            get => _loadError;

            set
            {
                if (_loadError != value)
                {
                    _loadError = value;
                    OnPropertyChanged(nameof(LoadError));
                }
            }
        }

        /// <summary>
        /// Flag which represents if the recipe list has finished loading.
        /// While false, an ActivityIndicator is displayed.
        /// </summary>
        public bool SearchComplete
        {
            get => _searchComplete;

            set
            {
                if (_searchComplete != value)
                {
                    _searchComplete = value;
                    OnPropertyChanged(nameof(SearchComplete));
                } 
            }
        }

        /// <summary>
        /// Flag which represents if a successful search returned any recipes.
        /// If no recipes were loaded, the user is notified of this fact.
        /// </summary>
        public bool SearchHasResults
        {
            get => _searchHasResults;

            set
            {
                if (_searchHasResults != value)
                {
                    _searchHasResults = value;
                    OnPropertyChanged(nameof(SearchHasResults));
                }
            }
        }

        /// <summary>
        /// The string to be displayed in the navigation bar.
        /// </summary>
        public string TitleString { get; }

        /// <summary>
        /// The string to be displayed if no recipes were loaded.
        /// </summary>
        public string NoResultsString { get; }

        /// <summary>
        /// The collection containing all the recipes to be displayed in the view.
        /// </summary>
        public ObservableCollection<Recipe> RecipeResults { get; private set; }

        private List<Ingredient> UserIngredients { get; set; }

        /// <summary>
        /// Opens a detailed recipe view for the selected recipe.
        /// Prevents double taps.
        /// </summary>
        /// <param name="recipeObject">The selected recipe.</param>
        private void OpenRecipe(object recipeObject)
        {
            var stack = _navigation.NavigationStack;
            if (stack.Count != 0 && stack[stack.Count - 1].GetType() == typeof(RecipeView))
            {
                return;
            }

            var recipe = (Recipe)recipeObject;
            var recipeViewModel = new RecipeViewModel(recipe, _localDataManager);

            if (_containsSavedItems)
            {
                recipeViewModel.RecipeUnsaved += ReloadData;
            }

            _navigation.PushAsync(new RecipeView(recipeViewModel));
        }

        /// <summary>
        /// Refreshes the RecipeResults collection when a saved item is removed.
        /// Also re-adds a recipe if it is added back to the collection before the page is closed.
        /// </summary>
        /// <param name="sender">The Recipe object which has been added or removed.</param>
        /// <param name="e">Null.</param>
        private void ReloadData(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var recipe = (Recipe)sender;

            if (recipe.IsSaved && !RecipeResults.Contains(recipe))
            {
                RecipeResults.Add(recipe);
                SortRecipeCollectionAlphabetically();
            }
            else if (!recipe.IsSaved && RecipeResults.Contains(recipe))
            {
                RecipeResults.Remove(recipe);
                SortRecipeCollectionAlphabetically();
            }

            SearchHasResults = RecipeResults.Count != 0;
        }

        /// <summary>
        /// Sorts the RecipeResults alphabetically.
        /// </summary>
        private void SortRecipeCollectionAlphabetically()
        {
            var list = RecipeResults.ToList();
            list.Sort(ObjectComparisons.SortByRecipeName);
            RecipeResults = new ObservableCollection<Recipe>(list);
            OnPropertyChanged(nameof(RecipeResults));
        }

        /// <summary>
        /// Calculates how many ingredients are missing and included for each recipe.
        /// </summary>
        /// <param name="recipes">The current list of recipes</param>
        private void CalculateMissingIngredients(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                var includedCount = 0;
                var missingCount = 0;

                // Checks if the user has enough of each recipe ingredient and updates the recipe ingredient counts.
                // For the Common unit type, the quantities are ignored.
                foreach (var recipeIngredient in recipe.Ingredients)
                {
                    var ingredientIncluded = false;
                    foreach (var userIngredient in UserIngredients)
                    {
                        if (recipeIngredient.Name.ToLower().Contains(userIngredient.Name.ToLower()) &&
                            ((string.Equals(recipeIngredient.Unit, userIngredient.StandardUnit.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                            userIngredient.QuantityStandardUnit >= recipeIngredient.Quantity) ||
                            userIngredient.Unit == Enums.Units.Common ||
                            string.Equals(recipeIngredient.Unit, userIngredient.StandardUnit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                        {
                            includedCount++;
                            ingredientIncluded = true;
                            break;
                        }
                    }

                    if (!ingredientIncluded)
                    {
                        missingCount++;
                    }
                }

                recipe.IngredientsIncluded = includedCount;
                recipe.IngredientsMissing = missingCount;
            }
        }
    }
}
