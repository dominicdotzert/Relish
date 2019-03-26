using SQLite;

namespace Relish.Models
{
    /// <summary>
    /// Simple ingredient model for storing web-scraped ingredient data.
    /// </summary>
    public class ReadonlyIngredient
    {
        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The standard unit for the ingredient
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// The standard ingredient for the ingredient in the standard unit.
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// The original ingredient string parsed from the crawled site.
        /// </summary>
        public string OriginalString { get; set; }

        /// <summary>
        /// Flag for whether the user has the ingredient.
        /// </summary>
        [Ignore]
        public bool UserHasIngredient { get; set; }
    }
}
