using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using TraceIt.Controls;
using TraceIt.iOS.Extensions;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace TraceIt.iOS.Renderers.Appearance_Trackers
{
    public class GradientShellPageTabBarAppearanceTracker : ShellTabBarAppearanceTracker
    {
        private GradientShellPage page;

        public GradientShellPageTabBarAppearanceTracker(GradientShellPage context)
        {
            page = context;
        }

        public override void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
            base.SetAppearance(controller, appearance);

            SetToolbar(controller);
        }

        private void SetToolbar(UITabBarController controller)
        {
            controller.TabBar.ItemPositioning = UITabBarItemPositioning.Fill;
            controller.TabBar.BackgroundImage = CreateGradientLayer(controller).ToUIImage();
        }

        private CAGradientLayer CreateGradientLayer(UITabBarController controller)
        {
            var tabBarGradient = new CAGradientLayer()
            {
                Colors = new[] {
                    page.BottomTabBarTopColor.ToCGColor(),
                    page.BottomTabBarBottomColor.ToCGColor() },

                Frame = controller.TabBar.Bounds,
                Locations = new NSNumber[] { 0, 1 },
                StartPoint = new CoreGraphics.CGPoint(0.5, 0),
                EndPoint = new CoreGraphics.CGPoint(0.5, 1)
            };

            return tabBarGradient;
        }
    }
}