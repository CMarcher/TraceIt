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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TraceIt.Droid.Renderers.Appearance_Trackers
{
    public class GradientShellToolbarAppearanceTracker : ShellToolbarAppearanceTracker
    {
        IShellContext context;

        public GradientShellToolbarAppearanceTracker(IShellContext context) : base(context)
        {
            this.context = context;
        }

        public override void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            base.SetAppearance(toolbar, toolbarTracker, appearance);

            var renderer = (GradientShellPageRenderer)context;
            var page = renderer.GetPageInstance();

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.LeftRight,
                new int[] {
                    page.ToolbarTopColor.ToAndroid(),
                    page.ToolbarBottomColor.ToAndroid() }
                );

            toolbar.SetBackground(gradient);
        }
    }
}