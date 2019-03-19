using System.Collections.Generic;
using SQLite;

namespace Relish.Models
{
    public class Recipe
    {
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

        public string Name { get; }

        public string ThumbnailUrl { get; }
        
        public string ImageUrl { get; }

        public string Url { get; }

        public List<string> Ingredients { get; }

        public List<string> Directions { get; }

        public int PrepTime { get; }

        public int CookTime { get; }

        public string Cuisine { get; }

        public string PrepStyle { get; }

        public string MealType { get; }

        public string ServingSize { get; }

        public bool IsMealPrepped { get; set; }

        public bool IsSaved { get; set; }
    }
}
