using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Triggers
{
    public class SelectedTriggerAction : TriggerAction<VisualElement>
    {
        public Color StartColor { get; set; } = Color.Red;
        public Color EndColor { get; set; } = Color.DarkRed;

        protected override void Invoke(VisualElement sender)
        {
            var button = sender as Button;

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
                Glyph = "\uf1f8",
                Color = Color.White
            };

            button.ImageSource = icon;
        }
    }
}
