using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Database;
using Relish.Models;
using Relish.Models.Filters;
using Relish.Views;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    public class StartSearchViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;
        private readonly INavigation _navigation;

        private string _keywordString;
        private bool _useAllIngredients = true;
        private int _prepTime;
        private int _cookTime;
        private string _cuisine;
        private string _mealType;
        private string _prepType;
        private bool _dataLoaded;

        public StartSearchViewModel(LocalDataManager localDataManager, INavigation navigation)
        {
            _localDataManager = localDataManager;
            _navigation = navigation;

            SelectedIngredients = new ObservableCollection<Ingredient>();
            Cuisine = Cuisines.All.ToString();
            PrepType = Enums.PrepTypes.All.ToString();
            MealType = Enums.MealType.All.ToString();

            SaveCommand = new Command(SaveFilterData);
            ClearFiltersCommand = new Command(ClearFilters);
            UseAllIngredientsCommand = new Command(UseAllIngredientsToggle);
            SpecifyIngredientCommand = new Command(SpecifyNewIngredient);
            RemoveIngredientCommand = new Command(RemoveIngredient);
            SearchCommand = new Command(BeginSearch);

            LoadData();
        }

        public ICommand SaveCommand { get; }

        public ICommand ClearFiltersCommand { get; }

        public ICommand UseAllIngredientsCommand { get; }

        public ICommand SpecifyIngredientCommand { get; }

        public ICommand RemoveIngredientCommand { get; }

        public ICommand SearchCommand { get; }

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

        public bool UseAllIngredients
        {
            get => _useAllIngredients;

            set
            {
                if (_useAllIngredients != value)
                {
                    _useAllIngredients = value; 
                    OnPropertyChanged(nameof(UseAllIngredients));
                    OnPropertyChanged(nameof(UseAllIngredientsText));
                }
            }
        }

        public string UseAllIngredientsText => _useAllIngredients ? "Yes" : "No";

        public List<Ingredient> Ingredients { get; private set; }

        public ObservableCollection<Ingredient> SelectedIngredients { get; set; }

        public int PrepTime
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

        public int CookTime
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

        public List<string> CuisineTypes { get; } = Enum.GetNames(typeof(Cuisines)).Select(x => x).ToList();

        public List<string> PrepTypes { get; } = Enum.GetNames(typeof(PrepTypes)).Select(x => x).ToList();

        public List<string> MealTypes { get; } = Enum.GetNames(typeof(MealType)).Select(x => x).ToList();

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

        public string PrepType
        {
            get => _prepType;

            set
            {
                if (_prepType != value)
                {
                    _prepType = value;
                    OnPropertyChanged(nameof(PrepType));
                }
            }
        }

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
                UseAllIngredients = filterData.UseAllIngredients;
                PrepTime = filterData.PrepTime;
                CookTime = filterData.CookTime;
                Cuisine = filterData.Cuisine;
                PrepType = filterData.PrepStyle;
                MealType = filterData.MealType;

                // Only add previously search ingredients if they still exist.
                // Add Ingredient object from current Ingredient list to ensure up to date quantity.
                foreach (var previousIngredient in filterData.SpecifiedIngredients)
                {
                    foreach (var currentIngredient in Ingredients)
                    {
                        if (previousIngredient.Name == currentIngredient.Name)
                        {
                            SelectedIngredients.Add(currentIngredient);
                        }
                    }
                }
            });

            // Update saved filter data in case an ingredient is no longer in inventory.
            SaveFilterData();
        }

        private void SaveFilterData()
        {
            var filterData = new FilterData
            {
                KeywordString = KeywordString?.Trim(),
                UseAllIngredients = UseAllIngredients,
                SpecifiedIngredients = SelectedIngredients.ToList(),
                PrepTime = PrepTime,
                CookTime = CookTime,
                Cuisine = Cuisine,
                PrepStyle = PrepType,
                MealType = MealType
            };
            _localDataManager.SaveFilterSettings(filterData);
        }

        private void ClearFilters()
        {
            KeywordString = string.Empty;
            UseAllIngredients = true;
            SelectedIngredients.Clear();
            PrepTime = 0;
            CookTime = 0;
            Cuisine = Cuisines.All.ToString();
            PrepType = Enums.PrepTypes.All.ToString();
            MealType = Enums.MealType.All.ToString();
            SaveFilterData();
        }

        private void UseAllIngredientsToggle()
        {
            UseAllIngredients = !UseAllIngredients;
        }

        private void SpecifyNewIngredient()
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count != 0)
            {
                return;
            }

            if (SelectedIngredients.Count == Ingredients.Count)
            {
                return;
            }

            var addNewIngredientAction = new Action<Ingredient>(i =>
            {
                SelectedIngredients.Add(i);
                OnPropertyChanged(nameof(SelectedIngredients));
            });
            var popup = new IngredientFilterPopup(
                SelectedIngredients.ToList(),
                Ingredients,
                addNewIngredientAction);

            PopupNavigation.Instance.PushAsync(popup);
        }

        private void RemoveIngredient(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            var ingredient = (Ingredient)parameter;
            SelectedIngredients.Remove(ingredient);
        }

        private async void BeginSearch()
        {
            if (!Validate())
            {
                return;
            }

            // Construct filters
            var filterList = new List<Filter>();

            if (!string.IsNullOrEmpty(KeywordString))
            {
                //filterList.Add(new KeywordFilter(FilterTypes.Keyword, KeywordString)));
            }

            if (UseAllIngredients)
            {
                //filterList.Add(new IngredientFilter());
            }
            else
            {
                //filterList.Add(new IngredientFilter());
            }

            if (PrepTime > 0)
            {
                filterList.Add(new TimeFilter(FilterTypes.PrepTime, PrepTime));
            }

            if (CookTime > 0)
            {
                filterList.Add(new TimeFilter(FilterTypes.CookTime, CookTime));
            }

            //filterList.Add(new CategoryFilter(FilterTypes.Cuisine, Cuisine));
            //filterList.Add(new CategoryFilter(FilterTypes.PrepStyle, PrepType));
            //filterList.Add(new CategoryFilter(FilterTypes.MealType, MealType));

            var query = new SearchQuery(filterList);
            await _navigation.PushAsync(new RecipeListView(query));
        }

        private bool Validate()
        {
            if (_navigation.NavigationStack[_navigation.NavigationStack.Count - 1].GetType() == typeof(RecipeListView))
            {
                return false;
            }

            // User must specify ingredients if they are not using all ingredients
            if (!UseAllIngredients && SelectedIngredients.Count == 0)
            {
                // TODO set error string
                return false;
            }

            if (Ingredients.Count == 0)
            {
                // TODO set error string
                SaveFilterData();
                return false;
            }

            return true;
        }
    }
}
