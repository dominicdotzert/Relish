using System.Collections.Generic;
using Relish.Resources;
using static Relish.Models.Enums;

namespace Relish.Utilities
{
    public static class EnumToFriendlyStringConverter
    {
        public static string FilterAttributeToString(FilterAttribute attribute)
        {
            switch (attribute)
            {
                case FilterAttribute.Ingredients:
                    return Strings.Filter_Ingredient;

                case FilterAttribute.PrepTime:
                    return Strings.Filter_PrepTime;

                case FilterAttribute.CookTime:
                    return Strings.Filter_CookTime;

                case FilterAttribute.Cuisine:
                    return Strings.Filter_Cuisine;

                case FilterAttribute.PrepStyle:
                    return Strings.Filter_PrepStyle;

                case FilterAttribute.MealType:
                    return Strings.Filter_MealType;

                default:
                    return attribute.ToString();
            }
        }
    }
}
