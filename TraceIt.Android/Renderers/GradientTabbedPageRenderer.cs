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
using TraceIt.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(GradientTabbedPage), typeof(GradientTabbedPageRenderer))]
namespace TraceIt.Droid.Renderers
{
    public class GradientTabbedPageRenderer : TabbedPageRenderer
    {
        public GradientTabbedPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            var control = (GradientTabbedPage)Element;

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.TopBottom,
                new int[] { control.TopColor.ToAndroid(), control.BottomColor.ToAndroid() }
                );

            var relativeLayout = this.GetChildAt(0) as Android.Widget.RelativeLayout;
            var bottomNavigationView = relativeLayout.GetChildAt(1) as BottomNavigationView;
            bottomNavigationView.SetBackground(gradient);
            bottomNavigationView.Elevation = 0;

        }
    }
}