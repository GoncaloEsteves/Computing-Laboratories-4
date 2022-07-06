using System;
using System.Globalization;
using Xamarin.Forms;

namespace PGB.Converters
{
    public class ChargerConverter : BaseCardValidator, IValueConverter
    {
        public ImageSource Fast { get; set; }
        public ImageSource Normal { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Normal;
                if ((bool) value) return Fast;
                    else { return Normal; };


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
