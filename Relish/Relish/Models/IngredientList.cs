using System.Collections.Generic;

namespace Relish.Models
{
    /// <summary>
    /// List<Ingredient> object with a Category label (for ListView grouping).
    /// </summary>
    public class IngredientList : List<Ingredient>
    {
        public IngredientList(Enums.IngredientCategories category)
        {
            Category = category.ToString();
        }

        public string Category { get; }

        public List<Ingredient> Ingredients => this;
    }
}
