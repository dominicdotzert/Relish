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
        public FilterData()
        {
            SpecifiedIngredients = new List<Ingredient>();
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string KeywordString { get; set; }

        public bool UseAllIngredients { get; set; }

        [TextBlob(nameof(SpecifiedIngredientsBlobbed))]
        public List<Ingredient> SpecifiedIngredients { get; set; }

        public string SpecifiedIngredientsBlobbed { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public string MealType { get; set; }

        public string PrepStyle { get; set; }

        public string Cuisine { get; set; }
    }
}
