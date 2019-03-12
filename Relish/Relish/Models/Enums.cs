namespace Relish.Models
{
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

        public enum FilterAttribute
        {
            Ingredients,
            PrepTime,
            CookTime,
            Cuisine,
            PrepStyle,
            MealType,
        }

        // TODO finish implementing IngredientsCategories enum
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

        public enum PrepTypes
        {
            All,
            SlowCooker,
            PanFried,
            BBQ
        }

        // TODO finish implementing Units enum
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
