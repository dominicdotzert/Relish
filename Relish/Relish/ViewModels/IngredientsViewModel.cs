using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Models;
using Relish.Resources;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    public class IngredientsViewModel : NotifyPropertyChanged
    {
        private readonly IngredientManager _ingredientManager;

        private ObservableCollection<IngredientList> _ingredientMasterList;
        private bool _loaded;
        private bool _editMode;
        private string _editText;

        public IngredientsViewModel(IngredientManager ingredientManager)
        {
            _ingredientManager = ingredientManager;
            _ingredientManager.DatabaseUpdated += UpdateList;

            EditToolbarText = Strings.Ingredients_Toolbar_Edit;

            EditToolbarCommand = new Command(EditButtonPressed);
            AddToolbarCommand = new Command(AddButtonPressed);
            RemoveIngredientCommand = new Command(RemoveIngredient);

            Task.Run(async () =>
            {
                IngredientMasterList = await LoadIngredientList();
                IngredientsLoaded = true;
            });
        }

        public bool IngredientsLoaded
        {
            get => _loaded;

            private set
            {
                if (_loaded != value)
                {
                    _loaded = value;
                    OnPropertyChanged(nameof(IngredientsLoaded));
                }
            }
        }

        public ObservableCollection<IngredientList> IngredientMasterList
        {
            get => _ingredientMasterList;

            set
            {
                if (_ingredientMasterList != value)
                {
                    _ingredientMasterList = value;
                    OnPropertyChanged(nameof(IngredientMasterList));
                }
            }
        }

        public bool EditMode
        {
            get => _editMode;

            private set
            {
                if (_editMode != value)
                {
                    _editMode = value;
                    OnPropertyChanged(nameof(EditMode));
                }
            }
        }

        public string EditToolbarText
        {
            get => _editText;

            private set
            {
                if (_editText != value)
                {
                    _editText = value;
                    OnPropertyChanged(nameof(EditToolbarText));
                }
            }
        }

        public ICommand EditToolbarCommand { get; }

        public ICommand AddToolbarCommand { get; }

        public ICommand RemoveIngredientCommand { get; }

        private async Task<ObservableCollection<IngredientList>> LoadIngredientList()
        {
            // Load all ingredients saved on the users device.
            var flatIngredientsList = await _ingredientManager.GetIngredients();

            // Initialize list of IngredientLists to required by ListView.
            var ingredientsList = new List<IngredientList>();

            // For each type of category, filter the flat ingredient list by that category.
            var categories = Enum.GetValues(typeof(IngredientCategories)).Cast<IngredientCategories>();
            foreach (var c in categories)
            {
                var categoryList = flatIngredientsList.Where(i => i.Category.Equals(c)).OrderBy(i => i.Name).ToList();
                if (categoryList.Count == 0)
                {
                    continue;
                }

                var list = new IngredientList(c);
                list.AddRange(categoryList);
                ingredientsList.Add(list);
            }

            return new ObservableCollection<IngredientList>(ingredientsList);
        }

        private void UpdateList(object sender, EventArgs e)
        {
            var ingredient = (Ingredient)sender;

            // Check if item has changed
            if (ingredient.Id == 0)
            {
                // Check if category is already in IngredientMasterList
                if (IngredientMasterList.Any(i => i.Category.Equals(ingredient.Category.ToString())))
                {
                    // Add ingredient to the ObservableCollection
                    foreach (var list in IngredientMasterList)
                    {
                        if (list.Category == ingredient.Category.ToString())
                        {
                            list.Ingredients.Add(ingredient);
                            list.Sort(OnIngredientComparison);
                            break;
                        }
                    }
                }
                // If not, add the category
                else
                {
                    var list = IngredientMasterList.ToList();
                    list.Add(new IngredientList(ingredient.Category) { ingredient });
                    list.Sort(OnComparison);

                    IngredientMasterList = new ObservableCollection<IngredientList>(list);
                    return;
                }
            }

            // Force ObservableCollection to update
            IngredientMasterList = new ObservableCollection<IngredientList>(IngredientMasterList.ToList());
        }

        private int OnIngredientComparison(Ingredient x, Ingredient y)
        {
            return string.Compare(x.Name, y.Name, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
        }

        private int OnComparison(IngredientList x, IngredientList y)
        {
            if (x.Category.Equals(y.Category))
            {
                return 0;
            }

            var a = (IngredientCategories)Enum.Parse(typeof(IngredientCategories), x.Category);
            var b = (IngredientCategories)Enum.Parse(typeof(IngredientCategories), y.Category);

            if (a < b)
            {
                return -1;
            }

            return 1;
        }

        private void EditButtonPressed()
        {
            EditMode = !EditMode;
            EditToolbarText = _editMode ? Strings.Ingredients_Toolbar_Done : Strings.Ingredients_Toolbar_Edit;
        }

        private void AddButtonPressed()
        {
            // Prevent double clicks by only allowing one popup to display at a time.
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count != 0)
            {
                return;
            }

            var ingredientList = IngredientMasterList.ToList();
            PopupNavigation.Instance.PushAsync(new IngredientPopup(new Ingredient(), _ingredientManager, ingredientList, true));
        }

        private async void RemoveIngredient(object parameter)
        {
            var ingredient = (Ingredient)parameter;

            // Remove from Database
            Task removeTask = _ingredientManager.RemoveIngredient(ingredient);

            // Remove from Collection
            for (int i = 0; i < IngredientMasterList.Count; i++)
            {
                if (IngredientMasterList[i].Category == ingredient.Category.ToString())
                {
                    IngredientMasterList[i].Ingredients.Remove(ingredient);
                    if (IngredientMasterList[i].Ingredients.Count == 0)
                    {
                        IngredientMasterList.RemoveAt(i);
                    }
                }
            }

            var list = IngredientMasterList.ToList();
            IngredientMasterList = new ObservableCollection<IngredientList>(list);

            if (list.Count == 0)
            {
                EditMode = false;
            }

            await removeTask;
        }
    }
}
