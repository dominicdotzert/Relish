using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class which returns false if the supplied integer value is equal to 0.
    /// </summary>
    public class ZeroIntToFalseConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            return val != 0;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
