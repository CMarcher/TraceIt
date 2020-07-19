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

[assembly: ExportRenderer(typeof(GradientShellPage), typeof(GradientShellPageRenderer))]
namespace TraceIt.iOS.Renderers
{
    public class GradientShellPageRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);

            if (!(renderer is null))
            {
                CreateGradients(
                    (renderer as ShellSectionRenderer).NavigationBar, 
                    (renderer as ShellSectionRenderer).TabBarController.TabBar);
            }

            return renderer;
        }

        private void CreateGradients(UINavigationBar navigationBar, UITabBar tabBar)
        {
            var control = (GradientShellPage)Element;

            #region NavigationBar
            var navigationGradient = new CAGradientLayer()
            {
                Colors = new[] {
                        control.ToolbarTopColor.ToCGColor(),
                        control.ToolbarBottomColor.ToCGColor() },

                Bounds = navigationBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }
            };

            navigationBar.BackgroundColor = UIColor.Clear;
            navigationBar.Layer.AddSublayer(navigationGradient); 
            #endregion

            #region TabBar
            var tabBarGradient = new CAGradientLayer()
            {
                Colors = new[] {
                        control.BottomTabBarTopColor.ToCGColor(),
                        control.BottomTabBarBottomColor.ToCGColor()
                    },

                Bounds = tabBar.Bounds,
                Locations = new NSNumber[] { 0, 1 }
            };

            tabBar.BackgroundColor = UIColor.Clear;
            tabBar.Layer.AddSublayer(tabBarGradient); 
            #endregion
        }
    }
}