using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Relish.Models
{
    public class Recipe
    {
        public Recipe()
        {
        }

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
            List<string> ingredients,
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

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }
        
        public string ImageUrl { get; set; }

        public string Url { get; set; }

        [TextBlob(nameof(IngredientsBlobbed))]
        public List<string> Ingredients { get; set; }

        public string IngredientsBlobbed { get; set; }

        [TextBlob(nameof(DirectionsBlobbed))]
        public List<string> Directions { get; set; }

        public string DirectionsBlobbed { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public string Cuisine { get; set; }

        public string PrepStyle { get; set; }

        public string MealType { get; set; }

        public string ServingSize { get; set; }

        public bool IsSaved { get; set; }
    }
}
