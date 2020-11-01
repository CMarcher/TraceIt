using P42.Utils;
using Syncfusion.ListView.XForms;
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
        public static ObservableCollection<GradeBreakdown> GetGradeBreakdowns(this IEnumerable<SelectedSubject> subjects)
        {
            var notAchieved = new GradeBreakdown(GradeBreakdown.Grades.NotAchieved);
            var achieved = new GradeBreakdown(GradeBreakdown.Grades.Achieved);
            var merit = new GradeBreakdown(GradeBreakdown.Grades.Merit);
            var excellence = new GradeBreakdown(GradeBreakdown.Grades.Excellence);
            var filteredSubjects = subjects.Where(subject => subject.Selected is true).ToList();
            var filteredStandards = filteredSubjects.GetSelectedStandards();

            notAchieved.SetCredits(filteredStandards);
            achieved.SetCredits(filteredStandards);
            merit.SetCredits(filteredStandards);
            excellence.SetCredits(filteredStandards);

            return new ObservableCollection<GradeBreakdown> { notAchieved, achieved, merit, excellence };
        }

        public static ObservableCollection<TotalBreakdown> GetTotalBreakdowns(this IEnumerable<SelectedSubject> subjects)
        {
            var selectedStandards = subjects.GetSelectedStandards();
            var levelOneBreakdown = new TotalBreakdown(selectedStandards, TotalBreakdown.Levels.One);
            var levelTwoBreakdown = new TotalBreakdown(selectedStandards, TotalBreakdown.Levels.Two);
            var levelThreeBreakdown = new TotalBreakdown(selectedStandards, TotalBreakdown.Levels.Three);
            var allBreakdown = new TotalBreakdown(selectedStandards, TotalBreakdown.Levels.All);

            var breakdowns = new ObservableCollection<TotalBreakdown>() 
                    { levelOneBreakdown, levelTwoBreakdown, levelThreeBreakdown, allBreakdown };

            return breakdowns;
        }

        public static ObservableCollection<SubjectEndorsement> GetSubjectEndorsements(this IEnumerable<SelectedSubject> subjects)
        {
            subjects = subjects.GetSelectedSubjects();
            var subjectEndorsements = new ObservableCollection<SubjectEndorsement>();

            Action<SelectedSubject> addEndorsements = subject =>
            {
                subjectEndorsements.Add(new SubjectEndorsement(subject, SubjectEndorsement.EndorsementTypes.Merit));
                subjectEndorsements.Add(new SubjectEndorsement(subject, SubjectEndorsement.EndorsementTypes.Excellence));
            };

            foreach(var subject in subjects)
                addEndorsements(subject);

            return subjectEndorsements;
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

        private static List<SelectedSubject> GetSelectedSubjects(this IEnumerable<SelectedSubject> subjects)
        {
            Predicate<SelectedSubject> subjectFilter = x => x.Selected is true;
            subjects = subjects.Where(new Func<SelectedSubject, bool>(subjectFilter));

            return subjects.ToList();
        }

        public static bool HasAchievedUniversityEntrance(this IEnumerable<SelectedSubject> subjects)
        {
            var selectedStandards = subjects.GetSelectedStandards();
            string lastSubjectName = "";
            Predicate<Standard> matchesLastSubject = x => x.Subject == lastSubjectName;
            Predicate<Standard> uniEntranceFilter = x => (int)x.FinalGrade >= 2 && x.Level is Standard.Levels.Three &&
                                                    x.StandardType is Standard.StandardTypes.Achievement;
            var creditsPerSubject = new List<int>();
            int currentIndex = -1;

            foreach(var standard in selectedStandards)
            {
                if (matchesLastSubject(standard) is false)
                {
                    lastSubjectName = standard.Subject;
                    creditsPerSubject.Add(new int());
                    currentIndex += 1;
                }

                if(uniEntranceFilter(standard))
                    creditsPerSubject[currentIndex] += standard.Credits;
            }

            creditsPerSubject = creditsPerSubject.OrderByDescending(x => x).Take(3).ToList();
            var total = creditsPerSubject.Sum();

            if (total < 36)
                return false;
            else
                return true;
        }

        public static Tuple<int, int> GetReadingAndWritingCredits(this IEnumerable<SelectedSubject> subjects)
        {
            var selectedStandards = subjects.GetSelectedStandards();
            var readingCredits = selectedStandards.CountCredits(x => (int)x.FinalGrade >= 2 && x.IsReading is true);
            var writingCredits = selectedStandards.CountCredits(x => (int)x.FinalGrade >= 2 && x.IsWriting is true);

            return new Tuple<int, int>(readingCredits, writingCredits);
        }
    }
}
