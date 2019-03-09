using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class for setting the Quantity entry field in the IngredientPopupView
    /// to be blank if the user specified unit is Common.
    ///
    /// Converts a negative quantity value to be an empty string.
    /// </summary>
    public class CommonUnitToEmptyStringConverter : IValueConverter
    {
        /// <inheritdoc />

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (float)value;

            return val < 0 ? string.Empty : val.ToString(CultureInfo.InstalledUICulture);
        }
        
        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
