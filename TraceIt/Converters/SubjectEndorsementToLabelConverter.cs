using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TraceIt.Models;
using Xamarin.Forms;

namespace TraceIt.Converters
{
    public class SubjectEndorsementToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            var subjectEndorsement = value as SubjectEndorsement;
            string level = GetLevel(subjectEndorsement);
            string endorsementType = GetEndorsementType(subjectEndorsement);

            string finalLabelText = subjectEndorsement.EndorsementType > SubjectEndorsement.EndorsementTypes.None ?
                "Endorsed at " + level + " with " + endorsementType : "Not endorsed";

            return finalLabelText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetLevel(SubjectEndorsement endorsement)
        {
            switch (endorsement.Level)
            {
                case SubjectEndorsement.Levels.One:
                    return "Level 1";
                case SubjectEndorsement.Levels.Two:
                    return "Level 2";
                case SubjectEndorsement.Levels.Three:
                    return "Level 3";
                default:
                    return "";
            }
        }

        private string GetEndorsementType(SubjectEndorsement endorsement)
        {
            switch (endorsement.EndorsementType)
            {
                case SubjectEndorsement.EndorsementTypes.Merit:
                    return "Merit";
                case SubjectEndorsement.EndorsementTypes.Excellence:
                    return "Excellence";
                default:
                    return "";
            }
        }
    }
}
