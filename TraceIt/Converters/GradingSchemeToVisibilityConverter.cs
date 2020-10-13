using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class GradingSchemeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var segmentType = (string)parameter;

            bool IsAchievedOnly = (Standard.GradeOptions)value == Standard.GradeOptions.AchievedOnly;
            bool IsUpToMerit = (Standard.GradeOptions)value == Standard.GradeOptions.UpToMerit;
            bool IsUpToExcellence = (Standard.GradeOptions)value == Standard.GradeOptions.UpToExcellence;

            bool IsMeritItem = segmentType == "MeritItem";
            bool IsExcellenceItem = segmentType == "ExcellenceItem";

            if (IsAchievedOnly && IsMeritItem)
                return false;
            else if (IsAchievedOnly && IsExcellenceItem)
                return false;

            else if (IsUpToMerit && IsMeritItem)
                return true;
            else if (IsUpToMerit && IsExcellenceItem)
                return false;

            else if (IsUpToExcellence)
                return true;
            else
                return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
