using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Triggers
{
    public class DeselectedTriggerAction : TriggerAction<VisualElement>
    {
        public Color StartColor { get; set; } = Color.Orange;
        public Color EndColor { get; set; } = Color.LightYellow;

        protected override void Invoke(VisualElement sender)
        {
            var button  = sender as Button;

            button.Background = new LinearGradientBrush()
            {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1),
                GradientStops = new GradientStopCollection()
                {
                    new GradientStop() { Offset = 0, Color = StartColor },
                    new GradientStop() { Offset = 1, Color = EndColor }
                }
            };

            var icon = new FontImageSource()
            {
                FontFamily = "PrimaryIcons",
                Glyph = "\ue804",
                Color = Color.Black
            };

            button.ImageSource = icon;
        }
    }
}
