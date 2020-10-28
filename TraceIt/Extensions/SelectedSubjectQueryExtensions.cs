using P42.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TraceIt.Models;
using TraceIt.Models.Query_Models;

namespace TraceIt.Extensions
{
    public static class SelectedSubjectQueryExtensions
    {
        public static ObservableCollection<CreditBreakdown> GetCreditBreakdowns(this IEnumerable<SelectedSubject> subjects)
        {
            var notAchieved = new CreditBreakdown(CreditBreakdown.Grades.NotAchieved);
            var achieved = new CreditBreakdown(CreditBreakdown.Grades.Achieved);
            var merit = new CreditBreakdown(CreditBreakdown.Grades.Merit);
            var excellence = new CreditBreakdown(CreditBreakdown.Grades.Excellence);
            var filteredSubjects = subjects.Where(subject => subject.Selected is true).ToList();
            var filteredStandards = filteredSubjects.GetSelectedStandards();

            notAchieved.SetCredits(filteredStandards);
            achieved.SetCredits(filteredStandards);
            merit.SetCredits(filteredStandards);
            excellence.SetCredits(filteredStandards);

            return new ObservableCollection<CreditBreakdown> { notAchieved, achieved, merit, excellence };
        }

        public static Tuple<int, int> GetAchievedAndTotalCredits(this IEnumerable<SelectedSubject> subjects)
        {
            int totalCredits = 0, achievedCredits = 0;
            Predicate<Standard> creditsFilter = standard => (int)standard.FinalGrade >= 2;
            var filteredSubjects = subjects.Where(subject => subject.Selected is true).ToList();

            foreach (var subject in filteredSubjects)
            {
                totalCredits += subject.Standards.CountCredits();
                achievedCredits += subject.Standards.CountCredits(creditsFilter);
            }

            return new Tuple<int, int>(totalCredits, achievedCredits);
        }

        public static ObservableCollection<Standard> GetSelectedStandards(this IEnumerable<SelectedSubject> subjects)
        {
            var standards = new ObservableCollection<Standard>();
            var filteredSubjects = subjects.Where(subject => subject.Selected is true).ToList();

            foreach (var subject in filteredSubjects)
            {
                var selectedStandards = subject.Standards.Where(standard => standard.Selected is true);
                standards.AddRange(selectedStandards);
            }

            return standards;
        }

        public static ObservableCollection<Level> GetAndSetLevels(this IEnumerable<SelectedSubject> subjects)
        {
            var levelOne = new Level(Level.Levels.One);
            var levelTwo = new Level(Level.Levels.Two);
            var levelThree = new Level(Level.Levels.Three);
            var standards = subjects.GetSelectedStandards();

            levelOne.SetAchievedCredits(standards);
            levelTwo.SetAchievedCredits(standards);
            levelThree.SetAchievedCredits(standards);

            return new ObservableCollection<Level>() { levelOne, levelTwo, levelThree };
        }

        public static Tuple<int, int> GetLiteracyAndNumeracyCredits(this IEnumerable<SelectedSubject> subjects)
        {
            var standards = subjects.GetSelectedStandards();
            Predicate<Standard> literacyCriteria = standard => (int)standard.FinalGrade >= 2 && standard.IsLiteracy;
            Predicate<Standard> numeracyCriteria = standard => (int)standard.FinalGrade >= 2 && standard.IsNumeracy;

            var literacyCredits = standards.CountCredits(literacyCriteria);
            var numeracyCredits = standards.CountCredits(numeracyCriteria);

            return new Tuple<int, int>(literacyCredits, numeracyCredits);
        }

        public static int GetRankScore(this IEnumerable<SelectedSubject> subjects)
        {
            Func<Standard, bool> rankScoreCriteria = standard =>
                (int)standard.FinalGrade >= 2 &&
                     standard.StandardType == Standard.StandardTypes.Achievement &&
                     standard.Level == Standard.Levels.Three;

            var filteredStandards = subjects.GetSelectedStandards()
                .Where(rankScoreCriteria)
                .OrderBy(x => x.Subject);

            var rankScoreSubjects = GetRankScoreSubjects(filteredStandards);

            rankScoreSubjects = rankScoreSubjects.OrderByDescending(x => x.RankScore).Take(5).ToList();
            int totalAchieved = rankScoreSubjects.CountCredits(x => x.FinalGrade == Standard.Grade.Achieved);
            int totalMerit = rankScoreSubjects.CountCredits(x => x.FinalGrade == Standard.Grade.Merit);
            int totalExcellence = rankScoreSubjects.CountCredits(x => x.FinalGrade == Standard.Grade.Excellence);

            Func<int, bool> totalCreditsZeroed = x => x is 0;
            while ((totalAchieved + totalMerit + totalExcellence) > 80)
            {
                if (totalCreditsZeroed(totalAchieved) is false)
                    totalAchieved--;
                else if (totalCreditsZeroed(totalExcellence) is false)
                    totalMerit--;
                else
                    totalExcellence--;
            }

            var achievedScore = totalAchieved * 2;
            var meritScore = totalMerit * 3;
            var excellenceScore = totalExcellence * 4;

            return achievedScore + meritScore + excellenceScore;
        }

        private static List<RankScoreSubject> GetRankScoreSubjects(IEnumerable<Standard> standards)
        {
            string lastSubjectName = "";
            var subjects = new List<RankScoreSubject>();
            Predicate<Standard> matchesLastSubject = x => x.Subject == lastSubjectName;
            RankScoreSubject currentSubject = new RankScoreSubject();

            foreach (var standard in standards)
            {
                if (matchesLastSubject(standard) is false)
                {
                    currentSubject = new RankScoreSubject();
                    lastSubjectName = standard.Subject;
                    subjects.Add(currentSubject);
                }

                currentSubject.AddStandard(standard);
            }

            foreach (var subject in subjects)
                subject.IntialiseRankScore();

            return subjects;
        }

        private static int CountCredits(this IEnumerable<RankScoreSubject> subjects, Predicate<Standard> criteria)
        {
            int credits = 0;

            foreach (var subject in subjects)
                credits += subject.Standards.CountCredits(criteria);

            return credits;
        }
    }
}
