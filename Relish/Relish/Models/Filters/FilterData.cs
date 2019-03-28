using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Class for saving previous user settings in the StartSearchView.
    /// </summary>
    public class FilterData
    {
        /// <summary>
        /// Initializes the Filter Data object.
        /// </summary>
        public FilterData()
        {
            SpecifiedIngredients = new List<Ingredient>();
        }

        /// <summary>
        /// The unique ID of the filter data object.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The string representing the user's keyword search.
        /// </summary>
        public string KeywordString { get; set; }

        /// <summary>
        /// The list of user specified primary search ingredients.
        /// </summary>
        [TextBlob(nameof(SpecifiedIngredientsBlobbed))]
        public List<Ingredient> SpecifiedIngredients { get; set; }

        /// <summary>
        /// A serialized string of the specified ingredients list for storage in the local db.
        /// </summary>
        public string SpecifiedIngredientsBlobbed { get; set; }

        /// <summary>
        /// The maximum prep time for a recipe specified by the user in integer minutes.
        /// </summary>
        public string PrepTime { get; set; }

        /// <summary>
        /// The maximum cook time for a recipe specified by the user in integer minutes.
        /// </summary>
        public string CookTime { get; set; }

        /// <summary>
        /// The user specified meal type for the recipe search.
        /// </summary>
        public string MealType { get; set; }

        /// <summary>
        /// The user specified preparation style for the recipe search.
        /// </summary>
        public string PrepStyle { get; set; }

        /// <summary>
        /// The user specified cuisine type for the recipe search.
        /// </summary>
        public string Cuisine { get; set; }
    }
}
