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
                SetToolbar();
        }

        private void SetToolbar()
        {
            Toolbar.Translucent = false;
            NavigationBar.BarTintColor = UIColor.Clear;
            Toolbar.BackgroundColor = UIColor.Clear;
            //Toolbar.SetBackgroundImage(gradient, UIToolbarPosition.Any, UIBarMetrics.Default);

            NavigationBar.Translucent = false;
            NavigationBar.BarTintColor = UIColor.Clear;
            NavigationBar.BackgroundColor = UIColor.Orange; 
            UINavigationBar.Appearance.BackgroundColor = UIColor.Blue;
            //NavigationBar.SetBackgroundImage(gradient, UIBarMetrics.Default);
        }

        private UIImage CreateGradientImage(GradientNavigationPage control)
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

            UIGraphics.BeginImageContext(gradient.Frame.Size);
            gradient.RenderInContext(UIGraphics.GetCurrentContext());
            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }
    }
}