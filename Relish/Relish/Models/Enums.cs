using System.Collections.Generic;

namespace Relish.Models
{
    public static class Enums
    {
        // TODO verify jon takes lowercase
        public static List<string> Cuisines = new List<string>
        {
            "All",
            "African",
            "Basque",
            "Belgian",
            "Brazilian",
            "British",
            "Cajun",
            "Cambodian",
            "Chinese",
            "Cowboy",
            "Creole",
            "Danish",
            "Ethiopian",
            "French",
            "German",
            "Greek",
            "Hawaiian",
            "Hungarian",
            "Indian",
            "Irish",
            "Italian",
            "Jamaican",
            "Japanese",
            "Jewish",
            "Korean",
            "Mediterranean diet",
            "Mexican",
            "Mexican and tex mex",
            "Middle eastern",
            "Moroccan cuisine",
            "New england",
            "New orleans",
            "Persian",
            "Polish",
            "Portuguese",
            "Provencal",
            "Puerto rican",
            "Southern",
            "Southwestern",
            "Spanish",
            "Swedish",
            "Texmex",
            "Thai",
            "Vietnamese"
        };

        public static List<string> MealTypes = new List<string>
        {
            "All",
            "Appetizer",
            "Breakfast and Brunch",
            "Dinner",
            "Dessert",
            "Drink",
            "Salad",
            "Sandwich",
            "Side dish",
            "Snack",
            "Soup",
            "Stew",
            "Soup and stew"
        };

        public static List<string> PrepStyles = new List<string>
        {
            "All",
            "1-pot",
            "Air fryer",
            "Baking",
            "Bbq",
            "Budget",
            "Candy",
            "Canning",
            "Casserole",
            "Comfort food",
            "Condiment",
            "Cookie",
            "Deep fried",
            "Dip",
            "Freezer-friendly",
            "Grill",
            "How to",
            "Instant pot",
            "Jams and jellies",
            "Kid-friendly",
            "Make-ahead",
            "Microwave",
            "Panini",
            "Pressure cooker",
            "Quick and easy",
            "Salsa",
            "Sauce",
            "Sheet pan dinner",
            "Slow cooker",
            "Sous vide",
            "Stir-fry"
        };

        public enum FilterTypes
        {
            Keyword,
            Ingredients,
            SpecifiedIngredients,
            PrepTime,
            CookTime,
            Cuisine,
            PrepStyle,
            MealType,
        }

        public enum IngredientCategories
        {
            Meat,
            Dairy,
            Produce,
            Pantry,
            Spices,
            Other
        }

        public enum Units
        {
            Quantity,
            g,
            kg,
            Oz,
            lb,
            mL,
            L,
            Tsp,
            Tbsp,
            Cup,
            Common,
        }
    }
}
