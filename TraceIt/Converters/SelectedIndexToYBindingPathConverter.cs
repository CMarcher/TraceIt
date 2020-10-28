using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TraceIt.Models.Query_Models;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class SelectedIndexToYBindingPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;

            if (index is 0)
                return nameof(CreditBreakdown.LevelOneCredits);
            else if (index is 1)
                return nameof(CreditBreakdown.LevelTwoCredits);
            else if (index is 2)
                return nameof(CreditBreakdown.LevelThreeCredits);
            else
                return nameof(CreditBreakdown.OverallCredits);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
