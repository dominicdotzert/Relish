using System.Collections.Generic;
using Relish.Utilities;

namespace Relish.Models.Filters
{
    public class IngredientFilter : Filter
    {
        private List<Ingredient> _ingredients;

        public IngredientFilter(Enums.FilterTypes filterType, List<Ingredient> ingredients)
            : base(filterType)
        {
            _ingredients = ingredients;
        }

        public override string ReturnQueryElement()
        {
            return string.Empty;
            if (_ingredients.Count == 0)
            {
                return string.Empty;
            }

            var ingredientList = new List<string>();

            foreach (var i in _ingredients)
            {
                ingredientList.Add($"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={i.Name}");
            }

            return string.Join("&", ingredientList);
        }
    }
}
