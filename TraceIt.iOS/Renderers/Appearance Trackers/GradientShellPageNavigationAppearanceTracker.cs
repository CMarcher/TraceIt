﻿using System;
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
    public class GradientShellPageNavigationAppearanceTracker : IShellNavBarAppearanceTracker
    {
        private bool disposedValue;

        private GradientShellPage page;

        public GradientShellPageNavigationAppearanceTracker(GradientShellPage context)
        {
            page = context;
        }

        public void ResetAppearance(UINavigationController controller)
        {

        }

        public void SetAppearance(UINavigationController controller, ShellAppearance appearance)
        {
            SetToolbar(controller);
        }

        private void SetToolbar(UINavigationController controller)
        {
            var gradientBackground = CreateGradient(controller).ToUIImage();
            var attributes = new UITextAttributes();
            attributes.TextColor = UIColor.White;

            UINavigationBar.Appearance.SetTitleTextAttributes(attributes);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            controller.NavigationBar.BackgroundColor = UIColor.Clear;
            controller.NavigationBar.SetBackgroundImage(gradientBackground, UIBarMetrics.Default);
        }

        private CAGradientLayer CreateGradient(UINavigationController controller)
        {
            var navigationGradient = new CAGradientLayer()
            {
                Colors = new[] {
                    page.ToolbarTopColor.ToCGColor(),
                    page.ToolbarBottomColor.ToCGColor() },

                Frame = controller.NavigationBar.Bounds,
                Locations = new NSNumber[] { 0, 1 },
                StartPoint = new CoreGraphics.CGPoint(0, 0.5),
                EndPoint = new CoreGraphics.CGPoint(1, 0.5)
            };

            return navigationGradient;
        }

        public void SetHasShadow(UINavigationController controller, bool hasShadow)
        {

        }

        public void UpdateLayout(UINavigationController controller) => SetToolbar(controller);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GradientShellPageNavigationAppearanceTracker()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}