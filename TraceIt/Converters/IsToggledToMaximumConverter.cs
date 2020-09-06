using Syncfusion.GridCommon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class IsToggledToMaximumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int normalValue = (string)parameter == "Subject" ? 14 : 50;
            int covidValue = (string)parameter == "Subject" ? 12 : 46;
            bool isCovidSetting = (bool)value;

            if (isCovidSetting)
                return covidValue;
            else
                return normalValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
