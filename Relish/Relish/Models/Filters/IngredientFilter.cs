using System.Collections.Generic;
using Relish.Utilities;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Model class to represent an Ingredient Filter.
    /// </summary>
    public class IngredientFilter : Filter
    {
        private readonly List<Ingredient> _ingredients;

        /// <summary>
        /// Initializes the Ingredient Filter object.
        /// </summary>
        /// <param name="filterType">The type of the Ingredient filter.</param>
        /// <param name="ingredients">The ingredients to be included in the search.</param>
        public IngredientFilter(Enums.FilterTypes filterType, List<Ingredient> ingredients)
            : base(filterType)
        {
            _ingredients = ingredients;
        }

        /// <summary>
        /// Gets the HTTP Get request element for the filter.
        /// </summary>
        /// <returns>Part of the HTTP GET query.</returns>
        public override string ReturnQueryElement()
        {
            if (_ingredients.Count == 0)
            {
                return string.Empty;
            }

            var ingredientList = new List<string>();

            foreach (var i in _ingredients)
            {
                ingredientList.Add(
                    $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={{\"name\":\"{i.Name}\",\"amount\":\"{i.QuantityStandardUnit}\",\"unit\":\"{i.StandardUnit.ToString()}\"}}");
            }

            return string.Join("&", ingredientList);
        }
    }
}
