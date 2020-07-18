using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using TraceIt.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TraceIt.Droid.Renderers.Appearance_Trackers
{
    public class GradientShellBottomTabBarAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        IShellContext context;

        public GradientShellBottomTabBarAppearanceTracker(IShellContext context, Xamarin.Forms.ShellItem item) : base(context, item)
        {
            this.context = context;
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            var renderer = (GradientShellPageRenderer)context;
            var page = renderer.GetPageInstance();

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.TopBottom,
                new int[] {
                    page.BottomTabBarTopColor.ToAndroid(),
                    page.BottomTabBarBottomColor.ToAndroid()
                });

            bottomView.SetBackground(gradient);

            
        }
    }
}