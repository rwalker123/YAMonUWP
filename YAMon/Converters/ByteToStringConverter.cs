using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace YAMon.Converters
{
    class ByteToStringConverter : IValueConverter
    {
        private const long ONEKB = 1024;
        private const long ONEMB = ONEKB * 1024;
        public const long ONEGB = ONEMB * 1024;
        public const long ONETB = ONEGB * 1024;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string unitsText = "B";

            double bytesToConvert = (double)((long)value);
            if (bytesToConvert >= ONEGB)
            {
                bytesToConvert /= ONEGB;
                unitsText = "GB";
            }
            else if (bytesToConvert >= ONEMB)
            {
                bytesToConvert /= ONEMB;
                unitsText = "MB";
            }
            else if (bytesToConvert >= ONEKB)
            {
                bytesToConvert /= ONEKB;
                unitsText = "KB";
            }

            return String.Format("{0} {1}", bytesToConvert.ToString("F1"), unitsText);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
