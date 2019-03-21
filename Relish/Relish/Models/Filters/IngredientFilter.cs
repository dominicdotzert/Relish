using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}
