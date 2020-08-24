using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Triggers
{
    public class DeselectedTriggerAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            var button  = sender as Button;

            button.BackgroundColor = Color.Yellow;
            button.TextColor = Color.Black;
            button.Text = "Add";
        }
    }
}
