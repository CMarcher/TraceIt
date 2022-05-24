using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class SfSegmentIsEnabledToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsEnabled = (bool)value;

            if (IsEnabled)
                return Color.FromHex("#0051AE");
            else
                return Color.FromHex("#525252");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
