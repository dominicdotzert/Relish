using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class to convert a string to be empty if the supplied integer value is equal to 0.
    /// </summary>
    public class ZeroIntToEmptyStringConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            return val == 0 ? string.Empty : val.ToString();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
