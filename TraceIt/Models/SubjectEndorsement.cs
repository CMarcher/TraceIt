using Syncfusion.ListView.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models
{
    public class SubjectEndorsement : BaseModel
    {
        public enum EndorsementTypes
        {
            None,
            Merit,
            Excellence
        }
        public enum Levels
        {
            None,
            One,
            Two,
            Three
        }

        public string Name { get; private set; }
        public int Year { get; private set; }
        public int MeritInternalCredits { get; private set; }
        public int ExcellenceInternalCredits { get; private set; }
        public int MeritExternalCredits { get; private set; }
        public int ExcellenceExternalCredits { get; private set; }
        public int MeritTotalCredits { get; private set; }
        public int ExcellenceTotalCredits { get; private set; }
        public EndorsementTypes EndorsementType { get; private set; }
        public Levels Level { get; private set; }

        IEnumerable<Standard> filteredStandards;
        int requiredCredits;

        public SubjectEndorsement(SelectedSubject subject)
        {
            Name = subject.BaseSubject.Name;
            Year = subject.Year;
            requiredCredits = Year is 2020 ? 12 : 14;

            SetCredits(subject);
            SetLevel();
            SetEndorsementType();
        }

        private void SetCredits(SelectedSubject subject)
        {
            var standardsFilter = GetGeneralFilter();
            filteredStandards = subject.Standards.Where(new Func<Standard, bool>(standardsFilter));

            MeritInternalCredits = filteredStandards.CountCredits(x => GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.Internal);
            MeritExternalCredits = filteredStandards.CountCredits(x => GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.External);
            MeritTotalCredits = MeritInternalCredits + MeritExternalCredits;

            ExcellenceInternalCredits = filteredStandards.CountCredits(x => x.FinalGrade is Standard.Grade.Excellence && x.AssessmentType is Standard.AssessmentTypes.Internal);
            ExcellenceExternalCredits = filteredStandards.CountCredits(x => x.FinalGrade is Standard.Grade.Excellence && x.AssessmentType is Standard.AssessmentTypes.External);
            ExcellenceTotalCredits = ExcellenceInternalCredits + ExcellenceExternalCredits;

            ExcellenceInternalCredits = Math.Min(ExcellenceInternalCredits, 3);
            ExcellenceExternalCredits = Math.Min(ExcellenceExternalCredits, 3);
            ExcellenceTotalCredits = ExcellenceTotalCredits - ExcellenceInternalCredits - ExcellenceExternalCredits;

            MeritInternalCredits = Math.Min(MeritInternalCredits, 3);
            MeritExternalCredits = Math.Min(MeritExternalCredits, 3);
            MeritTotalCredits = MeritTotalCredits - MeritInternalCredits - MeritExternalCredits;
        }

        private void SetEndorsementType()
        {
            var totals = GetTotalsForLevel();
            int meritInternalCredits = totals.Item1;
            int meritExternalCredits = totals.Item2;
            int meritTotal = totals.Item3;
            int excellenceInternalCredits = totals.Item4;
            int excellenceExternalCredits = totals.Item5;
            int excellenceTotal = totals.Item6;

            bool eligibleForMerit = meritInternalCredits >= 3 && meritExternalCredits >= 3 && meritTotal >= requiredCredits;
            bool eligibleForExcellence = excellenceInternalCredits >= 3 && excellenceExternalCredits >= 3 && excellenceTotal >= requiredCredits;

            if (eligibleForExcellence)
                EndorsementType = EndorsementTypes.Excellence;
            else if (eligibleForMerit)
                EndorsementType = EndorsementTypes.Merit;
            else
                EndorsementType = EndorsementTypes.None;
        }

        private void SetLevel()
        {
            var generalFilter = GetGeneralFilter();
            int levelOneCredits = filteredStandards.CountCredits(x => generalFilter(x) && x.Level is Standard.Levels.One);
            int levelTwoCredits = filteredStandards.CountCredits(x => generalFilter(x) && x.Level is Standard.Levels.Two);
            int levelThreeCredits = filteredStandards.CountCredits(x => generalFilter(x) && x.Level is Standard.Levels.Three);

            bool hasLevelThreeEndorsement = levelThreeCredits >= requiredCredits;
            bool hasLevelTwoEndorsement = (levelTwoCredits + levelThreeCredits) >= requiredCredits;
            bool hasLevelOneEndorsement = (levelOneCredits + levelTwoCredits + levelThreeCredits) >= requiredCredits;

            if (hasLevelThreeEndorsement)
                Level = Levels.Three;
            else if (hasLevelTwoEndorsement)
                Level = Levels.Two;
            else if (hasLevelOneEndorsement)
                Level = Levels.One;
            else
                Level = Levels.None;
        }

        private Predicate<Standard> GetGeneralFilter()
            => x => x.FinalGrade is Standard.Grade.Merit || x.FinalGrade is Standard.Grade.Excellence;

        private Tuple<int, int, int, int, int, int> GetTotalsForLevel()
        {
            Predicate<Standard> matchesLevel;
            switch (Level)
            {
                case Levels.One:
                    matchesLevel = x => x.Level >= Standard.Levels.One;
                    break;
                case Levels.Two:
                    matchesLevel = x => x.Level >= Standard.Levels.Two;
                    break;
                case Levels.Three:
                    matchesLevel = x => x.Level is Standard.Levels.Three;
                    break;
                default:
                    matchesLevel = x => x is null;
                    break;
            }

            int meritInternalCredits = filteredStandards.CountCredits(x => matchesLevel(x) 
                                                            && GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.Internal);
            int meritExternalCredits = filteredStandards.CountCredits(x => matchesLevel(x)
                                                            && GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.External);
            int meritTotal = filteredStandards.CountCredits(x => matchesLevel(x) && GetGeneralFilter()(x));

            int excellenceInternalCredits = filteredStandards.CountCredits(x => matchesLevel(x)
                                                            && GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.Internal);
            int excellenceExternalCredits = filteredStandards.CountCredits(x => matchesLevel(x)
                                                            && GetGeneralFilter()(x) && x.AssessmentType is Standard.AssessmentTypes.External);
            int excellenceTotal = filteredStandards.CountCredits(x => matchesLevel(x) && x.FinalGrade is Standard.Grade.Excellence);

            return new Tuple<int, int, int, int, int, int>(meritInternalCredits, meritExternalCredits, meritTotal, 
                                                           excellenceInternalCredits, excellenceExternalCredits, excellenceTotal);
        }
    }
}
