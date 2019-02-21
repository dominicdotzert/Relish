using Relish.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    public class CommonUnitToDisabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unit = (string)value;
            return unit != Enums.Units.Common.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
