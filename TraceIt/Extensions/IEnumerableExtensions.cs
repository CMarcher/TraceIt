using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TraceIt.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TResult> CastWithAction<TResult>(this IEnumerable list, Action<TResult> action)
        {
            IEnumerable<TResult> convertedList;

            foreach (var item in list) 
            {
                action.Invoke((TResult)item);
                convertedList.a
            }
        }
    }
}
