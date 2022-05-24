using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using TraceIt.Controls;
using TraceIt.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace TraceIt.iOS.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                Control.LeftView = new UIView(new CGRect(0, 0, 20, Control.Frame.Height));
                Control.RightView = new UIView(new CGRect(0, 0, 20, Control.Frame.Height));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;
            }

        }
    }
}