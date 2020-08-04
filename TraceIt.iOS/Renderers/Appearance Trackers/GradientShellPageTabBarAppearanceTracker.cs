using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using TraceIt.Controls;
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

            var tabBarGradient = new CAGradientLayer()
            {
                Colors = new[] {
                        page.BottomTabBarTopColor.ToCGColor(),
                        page.BottomTabBarBottomColor.ToCGColor() },

                Frame = controller.TabBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }  
            };

            controller.TabBar.BackgroundColor = UIColor.Clear;
            controller.TabBar.Layer.InsertSublayer(tabBarGradient, 1);
            controller.TabBar.ItemPositioning = UITabBarItemPositioning.Fill;
        }

        public override void UpdateLayout(UITabBarController controller)
        {
            base.UpdateLayout(controller);

            var tabBarGradient = new CAGradientLayer()
            {
                Colors = new[] {
                        page.BottomTabBarTopColor.ToCGColor(),
                        page.BottomTabBarBottomColor.ToCGColor()
                    },

                Frame = controller.TabBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }
            };

            controller.TabBar.BackgroundColor = UIColor.Clear;
            controller.TabBar.Layer.InsertSublayer(tabBarGradient, 1);
            controller.TabBar.ItemPositioning = UITabBarItemPositioning.Fill;
        }
    }
}