using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.XForms.TabView;
using TraceIt.Controls;
using TraceIt.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTabView), typeof(CustomTabViewRenderer))]
namespace TraceIt.Droid.Renderers
{
    internal class CustomTabViewRenderer : Syncfusion.XForms.Android.TabView.SfTabViewRenderer
    {
        public CustomTabViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SfTabView> e)
        {
            base.OnElementChanged(e);

            var scrollView = (HorizontalScrollView)Control.GetChildAt(0);
            var tabbar = scrollView.GetChildAt(0);
            

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.LeftRight,
                new int[] { Android.Graphics.Color.Blue, Android.Graphics.Color.Aqua }
                );

            gradient.SetCornerRadius(50);
            scrollView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            tabbar.SetBackground(gradient);
        }
    }
}