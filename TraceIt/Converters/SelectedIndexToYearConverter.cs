using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics;

namespace TraceIt.Converters
{
    public class SelectedIndexToYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int year = (int)value;

            return GetIndex(year);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;

            return GetYear(index);
        }

        private int GetIndex(int year)
        {
            if (year is 2017)
                return 0;
            if (year is 2018)
                return 1;
            if (year is 2019)
                return 2;
            if (year is 2020)
                return 3;
            if (year is 2021)
                return 4;
            else
                throw new Exception("Invalid year: " + year);
        }

        private int GetYear(int index)
        {
            try
            {
                if (index is 0)
                    return 2017;
                if (index is 1)
                    return 2018;
                if (index is 2)
                    return 2019;
                if (index is 3)
                    return 2020;
                if (index is 4)
                    return 2021;
                else
                    throw new Exception("Invalid index: " + index);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 2021;
            }
        }
    }
}
