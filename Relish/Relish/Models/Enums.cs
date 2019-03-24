namespace Relish.Models
{
    // TODO: Cindy to update enums.
    public static class Enums
    {
        public enum Cuisines
        {
            All,
            Mexican,
            Asian,
            Greek,
            French
        }

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

        public enum MealType
        {
            All,
            Lunch,
            Dinner,
        }

        public enum PrepStyles
        {
            All,
            SlowCooker,
            PanFried,
            BBQ
        }

        public enum Units
        {
            Quantity,
            g,
            kg,
            lbs,
            mL,
            L,
            Tsp,
            Tbsp,
            Cup,
            Oz,
            Common,
        }
    }
}
