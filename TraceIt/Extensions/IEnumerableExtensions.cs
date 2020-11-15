using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TraceIt.Models;

namespace TraceIt.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<TResult> ToListWithAction<TResult>(this IEnumerable list, Action<TResult> action)
        {
            var convertedList = new List<TResult>();

            foreach (var item in list)
            {
                action.Invoke((TResult)item);
                convertedList.Add((TResult)item);
            }

            return convertedList;
        }

        public static ObservableCollection<TResult> ToObservableCollection<TResult>(this IEnumerable<TResult> list)
        {
            var convertedCollection = new ObservableCollection<TResult>();

            foreach (var item in list)
                convertedCollection.Add(item);

            return convertedCollection;
        }

        public static void RemoveByID(this Collection<Standard> standards, Standard standard)
        {
            var search = standards.First(x => x.ID == standard.ID);
            standards.Remove(search);
        }

        public static int CountCredits(this IEnumerable<Standard> standards, Predicate<Standard> predicate = null)
        {
            int credits = 0;
            bool noFilterSet = predicate is null;

            foreach (var standard in standards)
            {
                bool matchesFilter = noFilterSet ? true : predicate(standard);
                if (matchesFilter)
                    credits += standard.Credits;
            }

            return credits;
        }

        public static bool IsEligibleForEndorsement(this IEnumerable<Standard> standards, int year)
        {
            int requiredTotalCredits = year is 2020 ? 12 : 14;

            Func<Standard, bool> criteria = standard => (int)standard.GradingScheme >= 2;
            var filteredStandards = standards.Where(criteria);
            var internalCredits = filteredStandards.CountCredits(x => x.AssessmentType is Standard.AssessmentTypes.Internal);
            var externalCredits = filteredStandards.CountCredits(x => x.AssessmentType is Standard.AssessmentTypes.External);
            var totalCredits = filteredStandards.CountCredits();

            bool isEligible = internalCredits >= 3 && externalCredits >= 3 && totalCredits >= requiredTotalCredits;
            return isEligible;
        }
    }
}
