using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    public class ZeroIntToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            return val == 0 ? string.Empty : val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
