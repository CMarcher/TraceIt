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
                var control = (GradientShellPage)Element;
                var navigationBar = (renderer as ShellSectionRenderer).NavigationBar;
                var tabBar = (renderer as ShellSectionRenderer).TabBarController.TabBar;


                if ((renderer as ShellSectionRenderer).NavigationBar != null)
                {
                    #region NavigationBar
                    var navigationGradient = new CAGradientLayer()
                    {
                        Colors = new[] {
                        control.ToolbarTopColor.ToCGColor(),
                        control.ToolbarBottomColor.ToCGColor() },

                        Bounds = navigationBar.Bounds,
                        Locations = new NSNumber[] { 0, 1 }
                    };

                    (renderer as ShellSectionRenderer).NavigationBar.BackgroundColor = UIColor.Clear;
                    (renderer as ShellSectionRenderer).NavigationBar.Layer.AddSublayer(navigationGradient);
                    #endregion
                }

                if ((renderer as ShellSectionRenderer).TabBarController.TabBar != null)
                {
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

                    (renderer as ShellSectionRootHeader).TabBarController.TabBar.BackgroundColor = UIColor.Clear;
                    (renderer as ShellSectionRootHeader).TabBarController.TabBar.Layer.AddSublayer(tabBarGradient);
                    #endregion
                }

            }

            return renderer;
        }

        public GradientShellPage GetPageInstance()
        {
            return (GradientShellPage)Element;
            
        }
    }
}