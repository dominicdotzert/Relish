using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Relish.Models
{
    /// <summary>
    /// Class to represent the Recipe Model.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Parameterless constructor is needed to initialize a Recipe table in the local SQLite database.
        /// </summary>
        public Recipe()
        {
        }

        /// <summary>
        /// Initializes a Recipe object.
        /// </summary>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="thumbnailUrl">The url of the thumbnail image.</param>
        /// <param name="imageUrl">The url of the main recipe image.</param>
        /// <param name="url">The source url of the recipe.</param>
        /// <param name="servingSize">The serving size of the recipe.</param>
        /// <param name="prepTime">The integer prep time in minutes.</param>
        /// <param name="cookTime">The integer cook time in minutes.</param>
        /// <param name="cuisine">The cuisine type of the recipe.</param>
        /// <param name="prepStyle">The preparation style of the recipe.</param>
        /// <param name="mealType">The meal type of the recipe.</param>
        /// <param name="ingredients">The list of ingredients required by the recipe.</param>
        /// <param name="directions">The directions to prepare the recipe.</param>
        public Recipe(
            string name,
            string thumbnailUrl,
            string imageUrl,
            string url,
            string servingSize,
            int prepTime,
            int cookTime,
            string cuisine,
            string prepStyle,
            string mealType,
            List<SimpleIngredient> ingredients,
            List<string> directions)
        {
            Name = name;
            ThumbnailUrl = thumbnailUrl;
            ImageUrl = imageUrl;
            Url = url;
            ServingSize = servingSize;
            PrepTime = prepTime;
            CookTime = cookTime;
            Cuisine = cuisine;
            PrepStyle = prepStyle;
            MealType = mealType;
            Ingredients = ingredients;
            Directions = directions;
        }

        /// <summary>
        /// The unique ID of the Recipe (for storing in the SQLite DB).
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The name of the recipe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The url of the thumbnail image.
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// The url of the main recipe image.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The source url of the recipe.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The list of ingredients required by the recipe.
        /// </summary>
        [TextBlob(nameof(IngredientsBlobbed))]
        public List<SimpleIngredient> Ingredients { get; set; }

        /// <summary>
        /// A serialized string of the ingredients list for storage in the local db.
        /// </summary>
        public string IngredientsBlobbed { get; set; }

        /// <summary>
        /// The directions to prepare the recipe.
        /// </summary>
        [TextBlob(nameof(DirectionsBlobbed))]
        public List<string> Directions { get; set; }

        /// <summary>
        /// A serialized string of the directions list for storage in the local db.
        /// </summary>
        public string DirectionsBlobbed { get; set; }

        /// <summary>
        /// The integer prep time in minutes.
        /// </summary>
        public int PrepTime { get; set; }

        /// <summary>
        /// The integer cook time in minutes.
        /// </summary>
        public int CookTime { get; set; }

        /// <summary>
        /// The cuisine type of the recipe.
        /// </summary>
        public string Cuisine { get; set; }

        /// <summary>
        /// The preparation style of the recipe.
        /// </summary>
        public string PrepStyle { get; set; }

        /// <summary>
        /// The meal type of the recipe.
        /// </summary>
        public string MealType { get; set; }

        /// <summary>
        /// The serving size of the recipe.
        /// </summary>
        public string ServingSize { get; set; }

        /// <summary>
        /// Flag which represents if the recipe has been saved by the user.
        /// </summary>
        public bool IsSaved { get; set; }

        [Ignore]
        public int IngredientsIncluded { get; set; }

        [Ignore]
        public int IngredientsMissing { get; set; }
    }
}
