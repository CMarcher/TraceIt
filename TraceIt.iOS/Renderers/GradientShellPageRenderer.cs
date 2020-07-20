using System;
using System.Collections.Generic;
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
        private GradientShellPage page = null;

        protected override void OnElementSet(Shell element)
        {
            base.OnElementSet(element);

            if (page is null)
                page = element as GradientShellPage;
        }

        protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
        {
            return new GradientShellPageNavigationAppearanceTracker((GradientShellPage)Element);
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new GradientShellPageTabBarAppearanceTracker((GradientShellPage)Element);
        }
    }
}