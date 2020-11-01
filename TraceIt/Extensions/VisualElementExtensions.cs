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
        public static void SetEnabledForAndroid(this VisualElement element, bool enableElement)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
                element.IsEnabled = enableElement is true ? true : false;
        }

        public static void SetEnabledForAndroid(this ToolbarItem toolbarItem, bool enableElement)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
                toolbarItem.IsEnabled = enableElement is true ? true : false;
        }
    }
}
