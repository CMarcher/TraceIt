using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using TraceIt.Controls;
using TraceIt.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientNavigationPage), typeof(GradientNavigationPageRenderer))]
namespace TraceIt.iOS.Renderers
{
    public class GradientNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var control = (GradientNavigationPage)Element;

            if (Element != null)
            {
                SetToolbar(CreateGradient(control));
            }   

        }

        private void SetToolbar(CAGradientLayer gradient)
        {
            Toolbar.Translucent = false;
            NavigationBar.BarTintColor = UIColor.Clear;
            Toolbar.BackgroundColor = UIColor.Clear;
            Toolbar.Layer.InsertSublayer(gradient, 1);

            NavigationBar.Translucent = false;
            NavigationBar.BarTintColor = UIColor.Clear;
            NavigationBar.BackgroundColor = UIColor.Clear;
            NavigationBar.Layer.InsertSublayer(gradient, 1);
        }

        private CAGradientLayer CreateGradient(GradientNavigationPage control)
        {
            var gradient = new CAGradientLayer()
            {
                Colors = new[]
                    {
                       control.TopColor.ToCGColor(),
                       control.BottomColor.ToCGColor()
                    },

                Frame = NavigationBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }
            };

            return gradient;
        }
    }
}