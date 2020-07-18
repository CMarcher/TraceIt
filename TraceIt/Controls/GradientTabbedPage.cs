using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Controls
{
    public class GradientTabbedPage : TabbedPage
    {
        public GradientTabbedPage()
        {
            BarTextColor = Color.White;
        }

        public static readonly BindableProperty BottomColorProperty =
        BindableProperty.Create(propertyName: nameof(BottomColor),
           returnType: typeof(Color),
           declaringType: typeof(GradientTabbedPage),
           defaultValue: Color.FromHex("#0033D5")); 

        public static readonly BindableProperty TopColorProperty =
            BindableProperty.Create(propertyName: nameof(TopColor),
                returnType: typeof(Color),
                declaringType: typeof(GradientTabbedPage),
                defaultValue: Color.FromHex("#002394"));

        public Color BottomColor
        {
            get { return (Color)GetValue(BottomColorProperty); }
            set { SetValue(BottomColorProperty, value); }
        }

        public Color TopColor
        {
            get { return (Color)GetValue(TopColorProperty); }
            set { SetValue(TopColorProperty, value); }
        }
    }
}
