using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TraceIt.Extensions
{
    public static class VisualElementExtensions
    {
        public static void SetEnabledForAndroid(this Element element, bool enableElement)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                if (element is Button button)
                    button.IsEnabled = enableElement is true ? true : false;
                else if (element is ToolbarItem toolbarItem)
                    toolbarItem.IsEnabled = enableElement is true ? true : false;
            }
                
        }
    }
}
