using System;
using System.Globalization;
using Relish.Models;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class for disabling the Quantity entry field in the IngredientPopupView
    /// if the user specified unit is Common.
    /// </summary>
    public class CommonUnitToDisabledConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unit = (string)value;
            return unit != Enums.Units.Common.ToString();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
