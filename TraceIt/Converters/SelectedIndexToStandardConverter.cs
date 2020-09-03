using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class SelectedIndexToStandardConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return Standard.Grade.NotAchieved;
                case 1:
                    return Standard.Grade.Achieved;
                case 2:
                    return Standard.Grade.Merit;
                case 3:
                    return Standard.Grade.Excellence;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((Standard.Grade)value)
            {
                case Standard.Grade.NotAchieved:
                    return 0;
                case Standard.Grade.Achieved:
                    return 1;
                case Standard.Grade.Merit:
                    return 2;
                case Standard.Grade.Excellence:
                    return 3;
                default:
                    return -1;
            }
        }
    }
}
