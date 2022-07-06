using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PGB.Converters
{
    public class FavoriteConverter : BaseCardValidator, IValueConverter
    {
        public ImageSource Favorite { get; set; }
        public ImageSource NotFavorite { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return NotFavorite;
            if ((bool)value) return Favorite;
            else { return NotFavorite; };


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
