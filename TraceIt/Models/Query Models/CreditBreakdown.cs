using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIt.Models.Query_Models
{
    public class CreditBreakdown
    {
        public enum Grades
        {
            NotAchieved,
            Achieved,
            Merit,
            Excellence
        }

        public int Credits { get; set; }
        public Grades Grade { get; set; }

        public CreditBreakdown(Grades grade)
        {
            Grade = grade;
        }

        public void AddCreditsFromSubject(SelectedSubject subject)
        {
            foreach(var standard in subject.Standards)
            {
                bool bothNotAchieved = standard.FinalGrade == Standard.Grade.NotAchieved &&
                                       Grade == Grades.NotAchieved;
                bool bothAchieved = standard.FinalGrade == Standard.Grade.Achieved &&
                                    Grade == Grades.Achieved;
                bool bothMerit = standard.FinalGrade == Standard.Grade.Merit &&
                                 Grade == Grades.Merit;
                bool bothExcellence = standard.FinalGrade == Standard.Grade.Excellence &&
                                      Grade == Grades.Excellence;
                bool canAddCredits = bothNotAchieved || bothAchieved || bothMerit || bothExcellence;

                if (canAddCredits)
                    Credits += standard.Credits;
            }
        }
        
    }
}
