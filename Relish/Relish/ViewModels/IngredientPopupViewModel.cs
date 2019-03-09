using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Relish.Models;
using Relish.Resources;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Relish.Models.Enums;

namespace Relish.ViewModels
{
    public class IngredientPopupViewModel : NotifyPropertyChanged
    {
        private readonly IngredientManager _ingredientManager;
        private readonly Ingredient _ingredient;
        private readonly List<IngredientList> _ingredientList;
        private readonly bool _newIngredient;

        private string _name;
        private string _category;
        private float _quantity;
        private string _unit;
        private string _ingredientNameError;
        private string _quantityError;

        public IngredientPopupViewModel(Ingredient ingredient, IngredientManager ingredientManager, List<IngredientList> ingredientList, bool newIngredient)
        {
            _ingredient = ingredient;
            _ingredientManager = ingredientManager;
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

        public List<string> AvailableUnits { get; } = Enum.GetNames(typeof(Units)).Select(x => x).ToList();

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

        public List<string> AvailableCategories { get; } =
            Enum.GetNames(typeof(IngredientCategories)).Select(x => x).ToList();

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

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        private async void ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }

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

                await _ingredientManager.SaveIngredient(_ingredient);
                ClosePopup();
            }
        }

        // Validate that all values are valid
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

            // Prevent duplicates within the same category
            // (only need to check for new ingredient case)
            if (_newIngredient &&
                _ingredientList
                    .Where(list => list.Category.Equals(Category))
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
