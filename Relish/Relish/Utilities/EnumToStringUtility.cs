using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Relish.Resources;
using static Relish.Models.Enums;

namespace Relish.Utilities
{
    // TODO add remaining enums
    public static class EnumToStringUtility
    {
        public static readonly Dictionary<FilterTypes, string> FilterTypeToQueryKeyDict =
            new Dictionary<FilterTypes, string>
            {
                { FilterTypes.Keyword, "keyword" },
                { FilterTypes.Ingredients, "ing" },
                { FilterTypes.SpecifiedIngredients, "ingredients"},
                { FilterTypes.PrepTime, "prepTime" },
                { FilterTypes.CookTime, "cookTime" },
                { FilterTypes.Cuisine, "cuisineType" },
                { FilterTypes.PrepStyle, "prepStyle" },
                { FilterTypes.MealType, "course" },
            };
        
        public static string FilterTypeToString(FilterTypes type)
        {
            switch (type)
            {
                case FilterTypes.Ingredients:
                    return Strings.Filter_Ingredient;

                case FilterTypes.PrepTime:
                    return Strings.Filter_PrepTime;

                case FilterTypes.CookTime:
                    return Strings.Filter_CookTime;

                case FilterTypes.Cuisine:
                    return Strings.Filter_Cuisine;

                case FilterTypes.PrepStyle:
                    return Strings.Filter_PrepStyle;

                case FilterTypes.MealType:
                    return Strings.Filter_MealType;

                default:
                    return type.ToString();
            }
        }
    }
}
