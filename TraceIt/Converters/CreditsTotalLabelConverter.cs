using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    class CreditsTotalLabelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is null || values[1] is null)
                return null;

            var passedCredits = (int)values[0];
            var totalCredits = (int)values[1];
            var convertType = (string)parameter;
            bool isSubjectsPageConversion = convertType == "SubjectsPage";

            string result = isSubjectsPageConversion ? passedCredits + " / " + totalCredits + " credits" :
                                                       passedCredits + " / " + totalCredits + " \ncredits";
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
