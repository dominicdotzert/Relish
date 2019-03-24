using System.Collections.Generic;
using Relish.Utilities;

namespace Relish.Models.Filters
{
    public class IngredientFilter : Filter
    {
        private readonly List<Ingredient> _ingredients;

        public IngredientFilter(Enums.FilterTypes filterType, List<Ingredient> ingredients)
            : base(filterType)
        {
            _ingredients = ingredients;
        }

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
                    $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={{\"name\":\"{i.Name}\",\"amount\":\"{i.Quantity}\",\"unit\":\"{i.Unit.ToString()}\"}}");
            }

            return string.Join("&", ingredientList);
        }
    }
}
