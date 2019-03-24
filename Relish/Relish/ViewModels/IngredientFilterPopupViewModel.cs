using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Relish.Models;
using Relish.Utilities;
using Relish.Views.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Relish.ViewModels
{
    /// <summary>
    /// ViewModel class to represent the user specified ingredient filter popup.
    /// </summary>
    public class IngredientFilterPopupViewModel : NotifyPropertyChanged
    {
        private readonly Action<Ingredient> _addIngredient;

        /// <summary>
        /// Initializes the view model object.
        /// </summary>
        /// <param name="specifiedIngredients">The list of user specified ingredients</param>
        /// <param name="allIngredients">The list of all the user's ingredients.</param>
        /// <param name="addIngredient">The action to specify a new ingredient.</param>
        public IngredientFilterPopupViewModel(List<Ingredient> specifiedIngredients, List<Ingredient> allIngredients, Action<Ingredient> addIngredient)
        {
            _addIngredient = addIngredient;

            IngredientTappedCommand = new Command(AddIngredient);
            CancelCommand = new Command(ClosePopup);

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

        /// <summary>
        /// Command for when an ingredient is tapped.
        /// </summary>
        public ICommand IngredientTappedCommand { get; }

        /// <summary>
        /// Command to close the popup.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// The list of unspecified ingredients.
        /// </summary>
        public ObservableCollection<Ingredient> UnselectedIngredients { get; }

        /// <summary>
        /// Specifies a new ingredient.
        /// </summary>
        /// <param name="ingredientObject">The ingredient to be specified.</param>
        private void AddIngredient(object ingredientObject)
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count > 0 && stack[stack.Count - 1].GetType() == typeof(IngredientFilterPopup))
            {
                var ingredient = (Ingredient)ingredientObject;
                _addIngredient(ingredient);
                PopupNavigation.Instance.PopAsync();
            }
        }

        /// <summary>
        /// Closes the popup.
        /// </summary>
        private void ClosePopup()
        {
            var stack = PopupNavigation.Instance.PopupStack;
            if (stack.Count > 0 && stack[stack.Count - 1].GetType() == typeof(IngredientFilterPopup))
            {
                PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
