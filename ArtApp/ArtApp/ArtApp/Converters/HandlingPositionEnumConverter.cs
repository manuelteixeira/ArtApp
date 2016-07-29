using System;
using System.Globalization;
using ArtApp.Model;
using Xamarin.Forms;

namespace ArtApp.Converters
{
    public class HandlingPositionEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((HandlingPosition)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : Enum.Parse(typeof(HandlingPosition), value.ToString());
        }
    }
}
