using System.Collections.Generic;
using System.Collections.ObjectModel;
using Relish.Models;
using Relish.Utilities;

namespace Relish.ViewModels
{
    public class IngredientFilterPopupViewModel : NotifyPropertyChanged
    {
        public IngredientFilterPopupViewModel(List<Ingredient> specifiedIngredients, List<Ingredient> allIngredients)
        {
            var specifiedIngredientsSet = new HashSet<Ingredient>(specifiedIngredients);
            var ingredients = new List<Ingredient>();

            foreach (var i in allIngredients)
            {
                if (!specifiedIngredientsSet.Contains(i))
                {
                    ingredients.Add(i);
                }
            }

            ingredients.Sort(IngredientComparisons.CompareIngredients);

            UnselectedIngredients = new ObservableCollection<Ingredient>(ingredients);
        }

        public ObservableCollection<Ingredient> UnselectedIngredients { get; }
    }
}
