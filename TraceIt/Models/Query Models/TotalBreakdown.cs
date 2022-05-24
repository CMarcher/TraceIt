using Syncfusion.GridCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models.Query_Models
{
    public class TotalBreakdown : BaseModel
    {
        public enum Levels
        {
            One,
            Two,
            Three,
            All
        }

        public Levels Level { get; private set; }

        public int TotalAchievedCredits { get; private set; }
        public int TotalCredits { get; private set; }

        public TotalBreakdown() { }

        public TotalBreakdown(IEnumerable<Standard> standards, Levels level)
        {
            Level = level;
            SetCredits(standards);
        }

        private void SetCredits(IEnumerable<Standard> standards)
        {
            var totalFilter = GetTotalFilter();
            TotalCredits = totalFilter != null ? standards.CountCredits(totalFilter) : standards.CountCredits();

            var achievedFilter = GetAchievedFilter();
            TotalAchievedCredits = standards.CountCredits(achievedFilter);
        }

        private Predicate<Standard> GetTotalFilter()
        {
            Predicate<Standard> levelOneFilter = x => x.Level == Standard.Levels.One;
            Predicate<Standard> levelTwoFilter = x => x.Level == Standard.Levels.Two;
            Predicate<Standard> levelThreeFilter = x => x.Level == Standard.Levels.Three;
            
            switch (Level)
            {
                case Levels.One:
                    return levelOneFilter;
                case Levels.Two:
                    return levelTwoFilter;
                case Levels.Three:
                    return levelThreeFilter;
                case Levels.All:
                    return null;
                default:
                    return null;
            }
        }

        private Predicate<Standard> GetAchievedFilter()
        {
            Predicate<Standard> levelOneAchievedFilter = x => (int)x.FinalGrade >= 2 && x.Level == Standard.Levels.One;
            Predicate<Standard> levelTwoAchievedFilter = x => (int)x.FinalGrade >= 2 && x.Level == Standard.Levels.Two;
            Predicate<Standard> levelThreeAchievedFilter = x => (int)x.FinalGrade >= 2 && x.Level == Standard.Levels.Three;
            Predicate<Standard> achievedFilter = x => (int)x.FinalGrade >= 2;

            switch (Level)
            {
                case Levels.One:
                    return levelOneAchievedFilter;
                case Levels.Two:
                    return levelTwoAchievedFilter;
                case Levels.Three:
                    return levelThreeAchievedFilter;
                case Levels.All:
                    return achievedFilter;
                default:
                    return null;
            }
        }
    }
}
