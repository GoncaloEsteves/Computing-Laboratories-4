using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PGB.Converters
{
    public class HasStationsConverter : BaseCardValidator, IValueConverter
    {
        public bool HasStations { get; set; }
        public bool DoesntHaveStations { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DoesntHaveStations;
            if ((int)value > 0) return HasStations;
            else { return DoesntHaveStations; };


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}