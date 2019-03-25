using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Relish.Models;

namespace Relish.Utilities
{
    /// <summary>
    /// Static utility class for defining Comparison methods for sorting List objects of non-primitive types.
    /// </summary>
    public static class ObjectComparisons
    {
        /// <summary>
        /// Comparison method for sorting Ingredients by name.
        /// </summary>
        /// <param name="x">Ingredient x.</param>
        /// <param name="y">Ingredient y.</param>
        /// <returns></returns>
        public static int CompareIngredients(Ingredient x, Ingredient y)
        {
            return string.Compare(x.Name, y.Name, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
        }

        /// <summary>
        /// Comparison method for sorting IngredientLists by Category.
        /// </summary>
        /// <param name="x">IngredientList x.</param>
        /// <param name="y">IngredientList y.</param>
        /// <returns></returns>
        public static int CompareIngredientLists(IngredientList x, IngredientList y)
        {
            if (x.Category.Equals(y.Category))
            {
                return 0;
            }

            var a = (Enums.IngredientCategories)Enum.Parse(typeof(Enums.IngredientCategories), x.Category);
            var b = (Enums.IngredientCategories)Enum.Parse(typeof(Enums.IngredientCategories), y.Category);

            if (a < b)
            {
                return -1;
            }

            return 1;
        }
        
        /// <summary>
        /// Comparison method for sorting Recipes by Name.
        /// </summary>
        /// <param name="x">Recipe x.</param>
        /// <param name="y">Recipe y.</param>
        /// <returns></returns>
        public static int SortByRecipeName(Recipe x, Recipe y)
        {
            return string.Compare(x.Name, y.Name, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        }
    }
}
