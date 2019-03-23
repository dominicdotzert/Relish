using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Relish.Data;
using Relish.Models;
using Relish.Resources;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    public class IngredientPopupViewModel : NotifyPropertyChanged
    {
        private readonly LocalDataManager _localDataManager;
        private readonly Ingredient _ingredient;
        private readonly List<IngredientList> _ingredientList;
        private readonly bool _newIngredient;

        private string _name;
        private string _category;
        private float _quantity;
        private string _unit;
        private string _ingredientNameError;
        private string _quantityError;

        /// <summary>
        /// Initializes new ingredient popup viewmodel.
        /// </summary>
        /// <param name="ingredient">The ingredient object. Default values if new.</param>
        /// <param name="localDataManager">The LocalDataManager object for saving ingredient data locally to device.</param>
        /// <param name="ingredientList">The current list of ingredients.</param>
        /// <param name="newIngredient">Flag for if the ingredient is new.</param>
        public IngredientPopupViewModel(Ingredient ingredient, LocalDataManager localDataManager, List<IngredientList> ingredientList, bool newIngredient)
        {
            _ingredient = ingredient;
            _localDataManager = localDataManager;
            _ingredientList = ingredientList;
            _newIngredient = newIngredient;

            _name = _ingredient.Name;
            _category = _ingredient.Category.ToString();
            _quantity = _ingredient.Quantity;
            _unit = _ingredient.Unit.ToString();

            // If adding new ingredient, set to -1 (so that view will render an empty Entry field)
            if (_newIngredient)
            {
                Quantity = -1f;
            }

            CancelCommand = new Command(ClosePopup);
            SaveCommand = new Command(Save);
        }

        /// <summary>
        /// Name of the ingredient.
        /// </summary>
        public string IngredientName
        {
            get => _name;

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(IngredientName));
                }
            }
        }

        /// <summary>
        /// Unit for the ingredient.
        /// Units are specified in the Units enum.
        /// </summary>
        public string Unit
        {
            get => _unit;

            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged(nameof(Unit));

                    if (_unit == Units.Common.ToString())
                    {
                        Quantity = -1f;
                    }
                }
            }
        }

        /// <summary>
        /// A list of all the units defined in the Units enum.
        /// </summary>
        public List<string> AvailableUnits { get; } = Enum.GetNames(typeof(Units)).Select(x => x).ToList();

        /// <summary>
        /// The quantity of the ingredient in the specified unit.
        /// Will be set to -1 if the "Common" unit type is specified.
        /// </summary>
        public float Quantity
        {
            get => _quantity;

            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        /// <summary>
        /// The category which the ingredient belongs to.
        /// Categories are specified in the IngredientCategories enum.
        /// </summary>
        public string Category
        {
            get => _category;

            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        /// <summary>
        /// A list of all the categories defined in the IngredientCategories enum.
        /// </summary>
        public List<string> AvailableCategories { get; } =
            Enum.GetNames(typeof(IngredientCategories)).Select(x => x).ToList();

        /// <summary>
        /// The error string for an invalid ingredient name.
        /// </summary>
        public string IngredientNameError
        {
            get => _ingredientNameError;

            set
            {
                if (_ingredientNameError != value)
                {
                    _ingredientNameError = value;
                    OnPropertyChanged(nameof(IngredientNameError));
                }
            }
        }

        /// <summary>
        /// The error string for an invalid quantity value.
        /// </summary>
        public string QuantityError
        {
            get => _quantityError;

            set
            {
                if (_quantityError != value)
                {
                    _quantityError = value;
                    OnPropertyChanged(nameof(QuantityError));
                }
            }
        }

        /// <summary>
        /// Command for when the user presses the Cancel button.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Command for when the user presses the Save button.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Removes the popup from the PopupNavigation stack.
        /// </summary>
        private async void ClosePopup()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        /// <summary>
        /// Validates the entries data and saves ingredient data locally to the device if valid.
        /// Closes the popup on success.
        /// </summary>
        private async void Save()
        {
            IngredientNameError = string.Empty;
            QuantityError = string.Empty;

            if (Validate())
            {
                _ingredient.Name = IngredientName.Trim();
                _ingredient.Category = (IngredientCategories)Enum.Parse(typeof(IngredientCategories), Category);
                _ingredient.Quantity = Quantity;
                _ingredient.Unit = (Units)Enum.Parse(typeof(Units), Unit);

                await _localDataManager.SaveIngredient(_ingredient);
                ClosePopup();
            }
        }

        /// <summary>
        /// Validates that an ingredient name has been entered.
        /// Validates that a positive quantity value has been entered (if the unit is not Common).
        /// For a new ingredient, validates that it is not a duplicate.
        /// </summary>
        /// <returns>Returns if all fields are valid.</returns>
        private bool Validate()
        {
            var result = true;

            // Check that a title has been supplied.
            if (string.IsNullOrEmpty(IngredientName))
            {
                IngredientNameError = Strings.IngredientsPopup_BlankNameError;
                result = false;
            }

            // Verify that a valid quantity has been entered
            if (Unit != Units.Common.ToString() && Quantity <= 0f)
            {
                QuantityError = Strings.IngredientsPopup_InvalidQuantityError;
                result = false;
            }

            // Prevent duplicates ingredients (only need to check for new ingredient case)
            if (_newIngredient &&
                _ingredientList
                    .SelectMany(list => list.Ingredients)
                    .Any(i => string.Compare(i.Name, IngredientName?.Trim(), CultureInfo.CurrentCulture, CompareOptions.IgnoreCase).Equals(0)))
            {
                IngredientNameError = Strings.IngredientsPopup_DuplicateIngredientError;
                result = false;
            }

            return result;
        }
    }
}
