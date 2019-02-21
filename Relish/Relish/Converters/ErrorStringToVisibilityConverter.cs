using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    public class ErrorStringToVisibilityConverter : IValueConverter
    {
        // Usage: for setting the IsVisible property of an error label based on the state of the error message
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
