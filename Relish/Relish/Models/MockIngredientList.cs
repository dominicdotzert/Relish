using System.Collections.Generic;
using static Relish.Models.Enums;

namespace Relish.Models
{
    public static class MockIngredientList
    {
        public static List<Ingredient> SimpleIngredientList = new List<Ingredient>
        {
            new Ingredient("Eggs", IngredientCategories.Meat, 12,  Units.Quantity),
            new Ingredient("Chicken Breast", IngredientCategories.Meat, 2, Units.Quantity),
            new Ingredient("Black Beans", IngredientCategories.Pantry, 500, Units.g),
            new Ingredient("Milk", IngredientCategories.Dairy, 1000, Units.mL),
        };

        public static List<Ingredient> SampleMeatList = new List<Ingredient>
        {
            new Ingredient("Chicken Breast", IngredientCategories.Meat, 3, Units.Quantity),
            new Ingredient("Pork Tenderloin", IngredientCategories.Meat, 2.5f, Units.lb),
            new Ingredient("Turkey Slices", IngredientCategories.Meat, 500, Units.g),
        };

        public static List<Ingredient> SampleDairyList = new List<Ingredient>
        {
            new Ingredient("Milk", IngredientCategories.Dairy, 4, Units.L),
            new Ingredient("Strawberry Yogurt", IngredientCategories.Dairy, 300, Units.mL)
        };

        public static List<Ingredient> SampleProduceList = new List<Ingredient>
        {
            new Ingredient("Tomatoes", IngredientCategories.Produce, 5, Units.Quantity),
            new Ingredient("Lettuce", IngredientCategories.Produce),
            new Ingredient("Onion", IngredientCategories.Produce, 24, Units.Quantity),
            new Ingredient("Bell Pepper", IngredientCategories.Produce, 3, Units.Quantity),
        };
    }
}
