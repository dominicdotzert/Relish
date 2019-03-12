using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Relish.Converters
{
    public class IsEnabledOpacityConverter : IValueConverter
    {
        private const double DisabledOpacity = 0.25;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isEnabled = (bool)value;
            return !isEnabled ? 1.0 : DisabledOpacity;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
