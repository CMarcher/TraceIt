using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraceIt.Extensions;

namespace TraceIt.Models.Query_Models
{
    public class GradeBreakdown
    {
        public enum Grades
        {
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        public int OverallCredits { get; set; }
        public int LevelOneCredits { get; set; }
        public int LevelTwoCredits { get; set; }
        public int LevelThreeCredits { get; set; }
        public Grades Grade { get; set; }

        public GradeBreakdown(Grades grade)
        {
            Grade = grade;
        }

        public void SetCredits(IEnumerable<Standard> standards)
        {
            var gradeFilter = GetFilter();
            Predicate<Standard> levelOneFilter = x => x.Level == Standard.Levels.One;
            Predicate<Standard> levelTwoFilter = x => x.Level == Standard.Levels.Two;
            Predicate<Standard> levelThreeFilter = x => x.Level == Standard.Levels.Three;
            var levelOneStandards = standards.Where(new Func<Standard, bool>(levelOneFilter));
            var levelTwoStandards = standards.Where(new Func<Standard, bool>(levelTwoFilter));
            var levelThreeStandards = standards.Where(new Func<Standard, bool>(levelThreeFilter));

            OverallCredits = standards.CountCredits(gradeFilter);
            LevelOneCredits = levelOneStandards.CountCredits(gradeFilter);
            LevelTwoCredits = levelTwoStandards.CountCredits(gradeFilter);
            LevelThreeCredits = levelThreeStandards.CountCredits(gradeFilter);
        }

        private Predicate<Standard> GetFilter()
        {
            switch (Grade)
            {
                case Grades.NotAchieved:
                    return x => x.FinalGrade == Standard.Grade.NotAchieved;
                case Grades.Achieved:
                    return x => x.FinalGrade == Standard.Grade.Achieved;
                case Grades.Merit:
                    return x => x.FinalGrade == Standard.Grade.Merit;
                case Grades.Excellence:
                    return x => x.FinalGrade == Standard.Grade.Excellence;
                default:
                    throw new Exception("CreditBreakdown's Grade property not set");
            }
        }
        
    }
}
