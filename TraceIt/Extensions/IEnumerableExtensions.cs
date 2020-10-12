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

        public static ObservableCollection<TResult> ToObservableCollection<TResult>(this IEnumerable list)
        {
            var convertedCollection = new ObservableCollection<TResult>();

            foreach(var item in list)
            {
                convertedCollection.Add((TResult)item);
            }

            return convertedCollection;
        }

        public static void ClearCredits(this IEnumerable<Endorsement> endorsements)
        {
            foreach (var endorsement in endorsements)
                endorsement.ClearCredits();
        }

        public static void RemoveByID(this Collection<Standard> standards, Standard standard)
        {
            var search = standards.First(x => x.ID == standard.ID);
            standards.Remove(search);
        }

        public static int CountCredits(this IEnumerable<Standard> standards, Func<Standard, bool> predicate)
        {
            int credits = 0;

            foreach(var standard in standards)
            {
                bool matchesCriteria = predicate(standard);
                if (matchesCriteria)
                    credits += standard.Credits;
            }

            return credits;
        }
    }
}
