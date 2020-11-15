using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class GradeEnumToLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Standard.Grade)value)
            {
                case Standard.Grade.NoGrade:
                    return "No Grade";
                case Standard.Grade.NotAchieved:
                    return "NA";
                case Standard.Grade.Achieved:
                    return "A";
                case Standard.Grade.Merit:
                    return "M";
                case Standard.Grade.Excellence:
                    return "E";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
