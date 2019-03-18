using System.Collections.Generic;
using SQLite;

namespace Relish.Models
{
    public class Recipe
    {
        public Recipe(
            string name,
            string imageUrl,
            string servingSize,
            int prepTime,
            int cookTime)
        {
            Name = name;
            ImageUrl = imageUrl;
            ServingSize = servingSize;
            PrepTime = prepTime;
            CookTime = cookTime;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; }
        
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
    }
}
