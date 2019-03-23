﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace Relish.Converters
{
    /// <summary>
    /// Converter class to control the opacity of a ViewElement depending on the state
    /// of the boolean flag controlling whether it is enabled or not.
    /// </summary>
    public class IsEnabledOpacityConverter : IValueConverter
    {
        private const double DisabledOpacity = 0.25;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isEnabled = (bool)value;
            return !isEnabled ? 1.0 : DisabledOpacity;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
