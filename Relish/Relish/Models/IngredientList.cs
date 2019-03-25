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

        /// <summary>
        /// The Ingredient Category for the list.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// The list of ingredients.
        /// </summary>
        public List<Ingredient> Ingredients => this;
    }
}
