using System.Collections.Generic;

namespace Relish.Models
{
    public class Recipe
    {
        public Recipe(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public string Name { get; }
        
        public string ImageUrl { get; }

        public string Url { get; }

        public List<string> Ingredients { get; set; }

        public List<string> Directions { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public string Cuisine { get; set; }

        public string PrepStyle { get; set; }

        public string MealType { get; set; }

        public string ServingSize { get; set; }
    }
}
