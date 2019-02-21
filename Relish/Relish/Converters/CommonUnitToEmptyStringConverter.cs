using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    public class CommonUnitToEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (float)value;

            return val < 0 ? string.Empty : val.ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
