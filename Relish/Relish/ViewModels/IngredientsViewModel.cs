using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Relish.Models;
using Relish.Resources;
using Relish.Utilities;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    public class IngredientsViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;

        private ObservableCollection<IngredientList> _ingredientMasterList;
        private bool _loaded;
        private bool _editMode;
        private string _editText;

        /// <summary>
        /// Initializes the ingredients page viewmodel.
        /// </summary>
        /// <param name="localDataManager">The LocalDataManager object for accessing saved ingredient data.</param>
        public IngredientsViewModel(LocalDataManager localDataManager)
        {
            _localDataManager = localDataManager;
            _localDataManager.IngredientTableUpdated += UpdateList;

            EditToolbarText = Strings.Ingredients_Toolbar_Edit;

            EditToolbarCommand = new Command(EditButtonPressed);
            AddToolbarCommand = new Command(AddButtonPressed);
            RemoveIngredientCommand = new Command(RemoveIngredient);
            EditIngredientCommand = new Command(EditIngredient);

            Task.Run(async () =>
            {
                IngredientMasterList = await LoadIngredientList();
                IngredientsLoaded = true;
            });
        }

        /// <summary>
        /// Flag for if the Ingredients have finished loading.
        /// When false, a loading animation will be displayed.
        /// </summary>
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

        /// <summary>
        /// Observable collection for all saved ingredients.
        /// Sorted by ingredient category (in order specified in the IngredientCategories enum),
        /// then alphabetically by ingredient name under each category.
        /// </summary>
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

        /// <summary>
        /// Flag for if user has entered the edit mode (by pressing the Edit button).
        /// When in edit mode, delete buttons will be displayed instead of unit and quantity information.
        /// </summary>
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

        /// <summary>
        /// String for Edit toolbar button.
        /// </summary>
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

        /// <summary>
        /// Command for pressing the Edit toolbar button.
        /// </summary>
        public ICommand EditToolbarCommand { get; }

        /// <summary>
        /// Command for removing an ingredient item.
        /// </summary>
        public ICommand RemoveIngredientCommand { get; }

        /// <summary>
        /// Command for pressing the Add toolbar button.
        /// </summary>
        public ICommand AddToolbarCommand { get; }

        /// <summary>
        /// Command for editing an ingredient item.
        /// </summary>
        public ICommand EditIngredientCommand { get; }

        /// <summary>
        /// Loads the saved ingredient data from the local device database.
        /// </summary>
        /// <returns>The ObservableCollection containing the previously saved ingredients.</returns>
        private async Task<ObservableCollection<IngredientList>> LoadIngredientList()
        {
            // Load all ingredients saved on the users device.
            var flatIngredientsList = await _localDataManager.GetIngredients();

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

        /// <summary>
        /// Updates the ingredients collection whenever an ingredient is added or edited.
        /// </summary>
        /// <param name="sender">The ingredient which has been edited in the database.</param>
        /// <param name="e">EventArg is unused.</param>
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
                            list.Sort(IngredientComparisons.CompareIngredients);
                            break;
                        }
                    }
                }
                // If not, add the category
                else
                {
                    var list = IngredientMasterList.ToList();
                    list.Add(new IngredientList(ingredient.Category) { ingredient });
                    list.Sort(IngredientComparisons.CompareIngredientLists);

                    IngredientMasterList = new ObservableCollection<IngredientList>(list);
                    return;
                }
            }

            // Force ObservableCollection to update
            IngredientMasterList = new ObservableCollection<IngredientList>(IngredientMasterList.ToList());
        }

       /// <summary>
        /// Opens the ingredient popup for editing an existing ingredient.
        /// </summary>
        private void EditButtonPressed()
        {
            EditMode = !EditMode;
            EditToolbarText = _editMode ? Strings.Ingredients_Toolbar_Done : Strings.Ingredients_Toolbar_Edit;
        }

        /// <summary>
        /// Opens the ingredient popup for entering a new ingredient.
        /// </summary>
        private void AddButtonPressed()
        {
            // Prevent double clicks by only allowing one popup to display at a time.
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack[stack.Count-1].GetType() == typeof(IngredientPopup))
            {
                return;
            }

            var ingredientList = IngredientMasterList.ToList();
            PopupNavigation.Instance.PushAsync(new IngredientPopup(new Ingredient(), _localDataManager, ingredientList, true));
        }

        /// <summary>
        /// Opens the ingredient popup for editing an existing ingredient.
        /// </summary>
        /// <param name="ingredientObject">The ingredient to be edited.</param>
        private void EditIngredient(object ingredientObject)
        {
            var ingredient = (Ingredient)ingredientObject;

            // Prevent double clicks by only allowing one popup to display at a time.
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack[stack.Count - 1].GetType() == typeof(IngredientPopup))
            {
                return;
            }

            var ingredientList = IngredientMasterList.ToList();
            PopupNavigation.Instance.PushAsync(new IngredientPopup(ingredient, _localDataManager, ingredientList, false));
        }

        /// <summary>
        /// Removes an ingredient from both the ListView collection and from the local DB.
        /// If an ingredient category becomes empty, it will be removed from the view.
        /// If the ingredient list becomes empty, edit mode will be exited.
        /// </summary>
        /// <param name="ingredientObject">The ingredient to be removed.</param>
        private async void RemoveIngredient(object ingredientObject)
        {
            var ingredient = (Ingredient)ingredientObject;

            // Remove from Database
            Task removeTask = _localDataManager.RemoveIngredient(ingredient);

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

            // Leave edit mode if list becomes empty
            if (list.Count == 0)
            {
                EditMode = false;
            }

            await removeTask;
        }
    }
}
