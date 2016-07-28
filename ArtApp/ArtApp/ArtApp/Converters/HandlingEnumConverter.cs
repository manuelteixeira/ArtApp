using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtApp.Model;
using Xamarin.Forms;

namespace ArtApp.Converters
{
    public class HandlingEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Handling) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : Enum.Parse(typeof(Handling), value.ToString());
        }
    }
}
