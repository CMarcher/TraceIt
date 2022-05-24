using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    class CreditsTotalLabelConverter : IMultiValueConverter
    {
        public string Prefix { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is null || values[1] is null)
                return null;

            var passedCredits = (int)values[0];
            var totalCredits = (int)values[1];
            var convertType = (string)parameter;
            bool isSubjectsPageConversion = convertType == "SubjectsPage";
            bool isEndorsementsPageConversion = convertType == "EndorsementsPage";

            string subjectsResult = passedCredits + " / " + totalCredits + " credits";
            string otherResult = passedCredits + " / " + totalCredits + " \ncredits";
            string endorsementResult = Prefix + passedCredits + " / " + totalCredits + " credits";

            if (isSubjectsPageConversion)
                return subjectsResult;
            else if (isEndorsementsPageConversion)
                return endorsementResult;
            else
                return otherResult;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
