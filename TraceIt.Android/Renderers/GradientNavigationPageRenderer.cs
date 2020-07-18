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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using TraceIt.Controls;
using TraceIt.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(GradientNavigationPage), typeof(GradientNavigationPageRenderer))]
namespace TraceIt.Droid.Renderers
{
    
    public class GradientNavigationPageRenderer : NavigationPageRenderer
    {
        public GradientNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            DrawGradient();
        }

        protected override void OnWindowVisibilityChanged([GeneratedEnum] ViewStates visibility)
        {
            DrawGradient();
            base.OnWindowVisibilityChanged(visibility);

            
        }

        private void DrawGradient()
        {
            var control = (GradientNavigationPage)Element;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar == null) return;
            toolbar.SetBackground(new GradientDrawable(GradientDrawable.Orientation.LeftRight,
                new int[] { control.TopColor.ToAndroid(), control.BottomColor.ToAndroid() }));
        }
    }
}