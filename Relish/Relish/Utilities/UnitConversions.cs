using static Relish.Models.Enums;

namespace Relish.Utilities
{
    /// <summary>
    /// Utility class for handling unit conversions.
    /// </summary>
    public static class UnitConversions
    {
        /// <summary>
        /// Converts a quantity into its standard unit based on its specified unit.
        /// </summary>
        /// <param name="unit">The specified unit.</param>
        /// <param name="quantity">The quantity in the specified unit.</param>
        /// <returns>The quantity in the standard unit.</returns>
        public static float ConvertToStandardUnit(Units unit, float quantity)
        {
            switch (unit)
            {
                case Units.kg:
                    return quantity * 1000.0f; // to g

                case Units.L:
                    return quantity * 1000.0f; // to mL

                case Units.Oz:
                    return quantity * 28.35f; // to g

                case Units.lb:
                    return quantity * 453.592f; // to g

                case Units.Tsp:
                    return quantity * 4.929f; // to mL

                case Units.Tbsp:
                    return quantity * 14.787f; // to mL

                case Units.Cup:
                    return quantity * 236.588f; // to mL

                default:
                    return quantity; // unchanged
            }
        }

        /// <summary>
        /// Gets a standard unit based on the specified unit.
        /// </summary>
        /// <param name="unit">The non-standard unit.</param>
        /// <returns>The standard unit.</returns>
        public static Units GetStandardUnit(Units unit)
        {
            switch (unit)
            {
                case Units.Quantity:
                    return Units.Quantity;

                case Units.g:
                    return Units.g;

                case Units.kg:
                    return Units.g;

                case Units.Oz:
                    return Units.g;

                case Units.lb:
                    return Units.g;

                case Units.mL:
                    return Units.mL;

                case Units.L:
                    return Units.mL;

                case Units.Tsp:
                    return Units.mL;

                case Units.Tbsp:
                    return Units.mL;

                case Units.Cup:
                    return Units.mL;

                case Units.Common:
                    return Units.Common;

                default:
                    return Units.Quantity;
            }
        }
    }
}
