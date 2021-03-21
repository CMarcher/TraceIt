using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TraceIt.Extensions
{
    public static class NavigationExtensions
    {
        public static async Task TryPopModalAsync(this INavigation navigation, [CallerMemberName] string caller = null)
        {
            try
            {
                await navigation.PopModalAsync();
            }
            catch (Exception ex) 
            { 
                LogException(ex.Message, caller); 
            }
        }

        public static async Task TryPushModalAsync(this INavigation navigation, Page page, [CallerMemberName] string caller = null)
        {
            try
            {
                await navigation.PushModalAsync(page);
            }
            catch(Exception ex)
            {
                LogException(ex.Message, caller);
            }
        }

        private static void LogException(string message, string caller)
        {
            Debug.WriteLine("Exception: " + message + " (thrown by " + caller + ")");
        }
    }
}
