using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gestures;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TraceIt.Droid.Renderers.Appearance_Trackers
{
    public class GradientShellTopTabBarAppearanceTracker : ShellTabLayoutAppearanceTracker
    {
        IShellContext context;

        public GradientShellTopTabBarAppearanceTracker(IShellContext context) : base(context)
        {
            this.context = context;
        }

        public override void SetAppearance(TabLayout tabLayout, ShellAppearance appearance)
        {
            base.SetAppearance(tabLayout, appearance);

            var renderer = (GradientShellPageRenderer)context;
            var page = renderer.GetPageInstance();

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.LeftRight,
                new int[] {
                    page.ToolbarTopColor.ToAndroid(),
                    page.ToolbarBottomColor.ToAndroid() }
                );

            tabLayout.SetBackground(gradient);
            tabLayout.TabMode = 1;
            tabLayout.TabGravity = 0;
        }
    }
}