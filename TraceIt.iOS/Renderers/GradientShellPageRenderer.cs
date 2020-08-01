using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using TraceIt.Controls;
using TraceIt.iOS.Renderers;
using TraceIt.iOS.Renderers.Appearance_Trackers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientShellPage), typeof(GradientShellPageRenderer))]
namespace TraceIt.iOS.Renderers
{
    public class GradientShellPageRenderer : ShellRenderer
    {
        protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
        {
            return new GradientShellPageNavigationAppearanceTracker((GradientShellPage)Element);
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new GradientShellPageTabBarAppearanceTracker((GradientShellPage)Element);
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem item)
        {
            return base.CreateShellItemRenderer(item);
        }
    }
}