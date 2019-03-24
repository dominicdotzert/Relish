using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Relish.Models;
using Relish.Utilities;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    public class IngredientFilterPopupViewModel : NotifyPropertyChanged
    {
        private readonly Action<Ingredient> _addIngredient;

        public IngredientFilterPopupViewModel(List<Ingredient> specifiedIngredients, List<Ingredient> allIngredients, Action<Ingredient> addIngredient)
        {
            _addIngredient = addIngredient;

            IngredientTappedCommand = new Command(AddIngredient);

            var specifiedIngredientsSet = new HashSet<Ingredient>(specifiedIngredients);
            var ingredients = new List<Ingredient>();

            foreach (var i in allIngredients)
            {
                if (!specifiedIngredientsSet.Contains(i))
                {
                    ingredients.Add(i);
                }
            }

            ingredients.Sort(ObjectComparisons.CompareIngredients);

            UnselectedIngredients = new ObservableCollection<Ingredient>(ingredients);
        }

        public ICommand IngredientTappedCommand { get; }

        public ObservableCollection<Ingredient> UnselectedIngredients { get; }

        private void AddIngredient(object ingredientObject)
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                var ingredient = (Ingredient)ingredientObject;
                _addIngredient(ingredient);
                PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
