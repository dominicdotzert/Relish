using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class for checking if an error string is empty.
    ///
    /// Usage: sets the IsVisible property of an error label element to true if its error string is not empty.
    /// </summary>
    public class ErrorStringToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty((string)value);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
