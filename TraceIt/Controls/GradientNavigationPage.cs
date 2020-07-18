using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Controls
{
    public class GradientNavigationPage : NavigationPage
    {
        public GradientNavigationPage(Page page) : base(page)
        {
            BarTextColor = Color.White;
        }

        public static readonly BindableProperty BottomColorProperty =
        BindableProperty.Create(propertyName: nameof(BottomColor),
           returnType: typeof(Color),
           declaringType: typeof(GradientNavigationPage),
           defaultValue: Color.FromHex("#1392FF"));

        public static readonly BindableProperty TopColorProperty =
            BindableProperty.Create(propertyName: nameof(TopColor),
                returnType: typeof(Color),
                declaringType: typeof(GradientNavigationPage),
                defaultValue: Color.FromHex("#000C72"));

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
