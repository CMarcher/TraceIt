using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Triggers
{
    public class SelectedTriggerAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            var button = sender as Button;

            button.BackgroundColor = Color.Red;
            button.Text = "Remove";
            button.TextColor = Color.White;
        }
    }
}
