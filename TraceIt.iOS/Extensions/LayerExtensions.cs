using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using UIKit;

namespace TraceIt.iOS.Extensions
{
    public static class LayerExtensions
    {
        public static UIImage ToUIImage(this CALayer layer)
        {
            UIGraphics.BeginImageContext(layer.Frame.Size);
            layer.RenderInContext(UIGraphics.GetCurrentContext());
            UIImage output = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return output;
        }
    }
}