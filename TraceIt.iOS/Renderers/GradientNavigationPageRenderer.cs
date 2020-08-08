using System;
using System.Collections.Generic;
using System.Drawing;
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
                SetToolbar(CreateGradientLayer(control));
        }

        private void SetToolbar(CAGradientLayer gradient)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            NavigationController.NavigationBar.Layer.InsertSublayer(gradient, 1);
        }

        private CAGradientLayer CreateGradientLayer(GradientNavigationPage control)
        {
            var gradient = new CAGradientLayer()
            {
                Colors = new[]
                    {
                    control.TopColor.ToCGColor(),
                    control.BottomColor.ToCGColor()
                },

                Frame = NavigationController.NavigationBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }
            };

            return gradient;
        }
    }
}