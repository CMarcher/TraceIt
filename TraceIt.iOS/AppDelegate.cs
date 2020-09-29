using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.ListView.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.iOS.ProgressBar;

namespace TraceIt.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags("Brush_Experimental", "CarouselView_Experimental");
            Xamarin.Forms.Forms.Init();
            SfListViewRenderer.Init();
            SfBorderRenderer.Init();
            SfSwitchRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfCircularProgressBarRenderer.Init();
            SfButtonRenderer.Init();
            SfGradientViewRenderer.Init();
            SfTabViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            SfChartRenderer.Init();
            SfCheckBoxRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
