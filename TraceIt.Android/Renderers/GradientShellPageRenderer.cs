using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TraceIt.Controls;
using TraceIt.Droid.Renderers;
using TraceIt.Droid.Renderers.Appearance_Trackers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(GradientShellPage), typeof(GradientShellPageRenderer))]
namespace TraceIt.Droid.Renderers
{
    public class GradientShellPageRenderer : ShellRenderer
    {
        public GradientShellPageRenderer(Context context) : base(context) { }

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new GradientShellToolbarAppearanceTracker(this);
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new GradientShellBottomTabBarAppearanceTracker(this, shellItem);
        }

        protected override IShellTabLayoutAppearanceTracker CreateTabLayoutAppearanceTracker(ShellSection shellSection)
        {
            return new GradientShellTopTabBarAppearanceTracker(this);
        }

        public GradientShellPage GetPageInstance()
        {
            return (GradientShellPage)Element;
        }
    }
}