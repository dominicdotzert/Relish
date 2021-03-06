﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Relish.Models.Filters;
using Relish.Resources;
using Relish.Views;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    /// <summary>
    /// ViewModel class to represent the user's search filters screen.
    /// </summary>
    public class StartSearchViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;
        private readonly INavigation _navigation;

        private string _keywordString;
        private string _prepTime;
        private string _cookTime;
        private string _cuisine;
        private string _mealType;
        private string _prepStyle;
        private bool _dataLoaded;
        private string _errorString;

        /// <summary>
        /// Initializes the view model object.
        /// </summary>
        /// <param name="localDataManager">The LocalDataManager object.</param>
        /// <param name="navigation">The INavigation object for managing pages.</param>
        public StartSearchViewModel(LocalDataManager localDataManager, INavigation navigation)
        {
            _localDataManager = localDataManager;
            _navigation = navigation;

            SpecifiedIngredients = new ObservableCollection<Ingredient>();
            Cuisine = Cuisines[0];
            PrepStyle = Enums.PrepStyles[0];
            MealType = Enums.MealTypes[0];

            SaveCommand = new Command(SaveFilterData);
            ClearFiltersCommand = new Command(ClearFilters);
            SpecifyIngredientCommand = new Command(SpecifyNewIngredient);
            RemoveIngredientCommand = new Command(RemoveIngredient);
            SearchCommand = new Command(BeginSearch);

            LoadData();
        }

        /// <summary>
        /// Command for saving filter data.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Command for resetting filters to default values.
        /// </summary>
        public ICommand ClearFiltersCommand { get; }

        /// <summary>
        /// Command for specifying a new ingredient.
        /// </summary>
        public ICommand SpecifyIngredientCommand { get; }

        /// <summary>
        /// Command for removing a specified ingredient.
        /// </summary>
        public ICommand RemoveIngredientCommand { get; }

        /// <summary>
        /// Command to begin the search.
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Flag which represented if filter data and ingredients have finished loading from the local device.
        /// </summary>
        public bool DataLoaded
        {
            get => _dataLoaded;
            private set
            {
                if (_dataLoaded != value)
                {
                    _dataLoaded = value; 
                    OnPropertyChanged(nameof(DataLoaded));
                }
            }
        }

        /// <summary>
        /// String which represents the user's desired keyword search.
        /// </summary>
        public string KeywordString
        {
            get => _keywordString;

            set
            {
                if (_keywordString != value)
                {
                    _keywordString = value;
                    OnPropertyChanged(nameof(KeywordString));
                }
            }
        }

        /// <summary>
        /// List of the user's current ingredients.
        /// </summary>
        public List<Ingredient> Ingredients { get; private set; }

        /// <summary>
        /// List of ingredients to filter by in the search.
        /// </summary>
        public ObservableCollection<Ingredient> SpecifiedIngredients { get; set; }

        /// <summary>
        /// The maximum preparation time for the recipe search.
        /// </summary>
        public string PrepTime
        {
            get => _prepTime;

            set
            {
                if (_prepTime != value)
                {
                    _prepTime = value;
                    OnPropertyChanged(nameof(PrepTime));
                }
            }
        }

        /// <summary>
        /// The maximum cook time for the recipe search.
        /// </summary>
        public string CookTime
        {
            get => _cookTime;

            set
            {
                if (_cookTime != value)
                {
                    _cookTime = value;
                    OnPropertyChanged(nameof(CookTime));
                }
            }
        }

        /// <summary>
        /// The list of possible cuisine types.
        /// </summary>
        public List<string> CuisineTypes { get; } = Cuisines;

        /// <summary>
        /// The list of possible preparation styles.
        /// </summary>
        public List<string> PrepStyles { get; } = Enums.PrepStyles;

        /// <summary>
        /// The list of possible meal types.
        /// </summary>
        public List<string> MealTypes { get; } = Enums.MealTypes;

        /// <summary>
        /// The string representing th user's desired cuisine type.
        /// </summary>
        public string Cuisine
        {
            get => _cuisine;

            set
            {
                if (_cuisine != value)
                {
                    _cuisine = value;
                    OnPropertyChanged(nameof(Cuisine));
                }
            }
        }

        /// <summary>
        /// The string representing the user's desired preparation style.
        /// </summary>
        public string PrepStyle
        {
            get => _prepStyle;

            set
            {
                if (_prepStyle != value)
                {
                    _prepStyle = value;
                    OnPropertyChanged(nameof(PrepStyle));
                }
            }
        }

        /// <summary>
        /// The string representing the user's desired meal type.
        /// </summary>
        public string MealType
        {
            get => _mealType;

            set
            {
                if (_mealType != value)
                {
                    _mealType = value;
                    OnPropertyChanged(nameof(MealType));
                }
            }
        }

        /// <summary>
        /// The string representing any validation errors that might occur before starting the search.
        /// </summary>
        public string ErrorString
        {
            get => _errorString;

            set
            {
                if (_errorString != value)
                {
                    _errorString = value;
                    OnPropertyChanged(nameof(ErrorString));
                }
            }
        }

        /// <summary>
        /// Loads the user's ingredient data and previous search data.
        /// </summary>
        private void LoadData()
        {
            Task.Run(async () =>
            {
                var loadIngredientsTask = _localDataManager.GetIngredients();
                var loadFilterDataTask = _localDataManager.GetFilterSettings();
                await loadIngredientsTask;
                await loadFilterDataTask;

                DataLoaded = true;
                Ingredients = loadIngredientsTask.Result;

                // Parse filter data
                if (loadFilterDataTask.Result == null)
                {
                    return;
                }

                var filterData = loadFilterDataTask.Result;

                KeywordString = filterData.KeywordString;
                PrepTime = filterData.PrepTime;
                CookTime = filterData.CookTime;
                Cuisine = filterData.Cuisine;
                PrepStyle = filterData.PrepStyle;
                MealType = filterData.MealType;

                // Only add previously search ingredients if they still exist.
                // Add Ingredient object from current Ingredient list to ensure up to date quantity.
                foreach (var previousIngredient in filterData.SpecifiedIngredients)
                {
                    foreach (var currentIngredient in Ingredients)
                    {
                        if (previousIngredient.Name == currentIngredient.Name)
                        {
                            SpecifiedIngredients.Add(currentIngredient);
                        }
                    }
                }

                // Update saved filter data in case an ingredient is no longer in inventory.
                SaveFilterData();
            });
        }

        /// <summary>
        /// Saves the user entered filter to the local device.
        /// </summary>
        private void SaveFilterData()
        {
            if (!Validate())
            {
                return;
            }

            var filterData = new FilterData
            {
                KeywordString = KeywordString?.Trim(),
                SpecifiedIngredients = SpecifiedIngredients.ToList(),
                PrepTime = PrepTime,
                CookTime = CookTime,
                Cuisine = Cuisine,
                PrepStyle = PrepStyle,
                MealType = MealType
            };
            _localDataManager.SaveFilterSettings(filterData);
        }

        /// <summary>
        /// Resets all filters to their default value.
        /// </summary>
        private void ClearFilters()
        {
            KeywordString = string.Empty;
            SpecifiedIngredients.Clear();
            PrepTime = string.Empty;
            CookTime = string.Empty;
            Cuisine = Cuisines[0];
            PrepStyle = Enums.PrepStyles[0];
            MealType = Enums.MealTypes[0];
            SaveFilterData();
        }
        
        /// <summary>
        /// Opens the Ingredients Filter popup.
        /// </summary>
        private void SpecifyNewIngredient()
        {
            // Prevent button double clicks
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count != 0 && stack[stack.Count - 1].GetType() == typeof(IngredientFilterPopup))
            {
                return;
            }

            // TODO remove and replace with string in popup?
            // Don't open popup if all ingredients have already been specified
            if (SpecifiedIngredients.Count == Ingredients.Count)
            {
                return;
            }

            var addNewIngredientAction = new Action<Ingredient>(i =>
            {
                SpecifiedIngredients.Add(i);
                OnPropertyChanged(nameof(SpecifiedIngredients));
                SaveFilterData();
            });
            var popup = new IngredientFilterPopup(
                SpecifiedIngredients.ToList(),
                Ingredients,
                addNewIngredientAction);

            PopupNavigation.Instance.PushAsync(popup);
        }

        /// <summary>
        /// Removes the specified ingredient from the Specified Ingredients list.
        /// </summary>
        /// <param name="parameter">The ingredient to be removed.</param>
        private void RemoveIngredient(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            var ingredient = (Ingredient)parameter;
            SpecifiedIngredients.Remove(ingredient);
            SaveFilterData();
        }

        /// <summary>
        /// Gets all the filter objects and begins the search if the user has entered valid information.
        /// </summary>
        private async void BeginSearch()
        {
            // Prevent double clicks
            var stack = _navigation.NavigationStack;
            if (stack.Count != 0 && stack[stack.Count - 1].GetType() == typeof(RecipeListView))
            {
                return;
            }

            // Verify all filter inputs are valid.
            if (!Validate())
            {
                return;
            }

            // Construct filters.
            var filterList = new List<Filter>
            {
                // Comment out until filter is implemented server side.
                new IngredientFilter(FilterTypes.Ingredients, Ingredients)
            };

            if (SpecifiedIngredients.Count != 0)
            {
                filterList.Add(new IngredientFilter(FilterTypes.SpecifiedIngredients, SpecifiedIngredients.ToList()));
            }

            if (!string.IsNullOrEmpty(KeywordString))
            {
                filterList.Add(new KeywordFilter(FilterTypes.Keyword, KeywordString));
            }

            if (!string.IsNullOrEmpty(PrepTime))
            {
                var prepTime = int.Parse(PrepTime);
                if (prepTime > 0)
                {

                    filterList.Add(new TimeFilter(FilterTypes.PrepTime, prepTime));
                }
            }

            if (!string.IsNullOrEmpty(CookTime))
            {
                var cookTime = int.Parse(CookTime);
                if (cookTime > 0)
                {
                    filterList.Add(new TimeFilter(FilterTypes.CookTime, cookTime));
                }
            }

            if (Cuisine != Cuisines[0])
            {
                filterList.Add(new CategoryFilter(FilterTypes.Cuisine, Cuisine));
            }

            if (PrepStyle != Enums.PrepStyles[0])
            {
                filterList.Add(new CategoryFilter(FilterTypes.PrepStyle, PrepStyle)); 
            }

            if (MealType != Enums.MealTypes[0])
            {
                filterList.Add(new CategoryFilter(FilterTypes.MealType, MealType)); 
            }

            // Save filter data on valid search.
            SaveFilterData();

            // Begin search and open Recipe Results page.
            var query = new SearchQuery(filterList, _localDataManager);
            await _navigation.PushAsync(
                new RecipeListView(
                    query.StartSearch(),
                    _localDataManager,
                    Strings.Title_Results,
                    Strings.RecipeList_NoRecipesFound));
        }

        /// <summary>
        /// Validates that user entered data is valid.
        /// </summary>
        /// <returns>Returns if the filter information is valid.</returns>
        private bool Validate()
        {
            var result = true;
            var errors = new List<string>();
            ErrorString = string.Empty;

            if (Ingredients.Count == 0)
            {
                errors.Add(Strings.Error_NoIngredients);
                result = false;
            }

            if (!string.IsNullOrEmpty(PrepTime) &&
                (!int.TryParse(PrepTime, out var prepTime)
                || prepTime <= 0))
            {
                errors.Add(Strings.Error_BadPrepTime);
                result = false;
            }

            if (!string.IsNullOrEmpty(CookTime) &&
                (!int.TryParse(CookTime?.Trim(), out var cookTime) ||
                cookTime <= 0))

            {
                errors.Add(Strings.Error_BadCookTime);
                result = false;
            }

            ErrorString = string.Join("\n", errors);

            return result;
        }
    }
}
