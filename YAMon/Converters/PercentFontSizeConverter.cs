using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace YAMon.Converters
{
    class PercentFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int defaultFontSize = Int32.Parse(parameter.ToString());
            double percentUsage = (double)value;
            int newFontSize = (int)(defaultFontSize + ((double)defaultFontSize * percentUsage));
            return newFontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
