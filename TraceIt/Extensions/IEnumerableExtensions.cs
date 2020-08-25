using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

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
    }
}
