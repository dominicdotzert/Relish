using System.Collections.Generic;
using Relish.Resources;
using static Relish.Models.Enums;

namespace Relish.Utilities
{
    // TODO add remaining enums
    public static class EnumToFriendlyStringConverter
    {
        public static string FilterAttributeToString(FilterTypes type)
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
