using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Controls
{
    public class GradientShellPage : Shell
    {
        public GradientShellPage() { }

        public static readonly BindableProperty ToolbarBottomColorProperty =
        BindableProperty.Create(propertyName: nameof(ToolbarBottomColor),
           returnType: typeof(Color),
           declaringType: typeof(GradientShellPage),
           defaultValue: Color.FromHex("#1392FF")); 

        public static readonly BindableProperty ToolbarTopColorProperty =
            BindableProperty.Create(propertyName: nameof(ToolbarTopColor),
                returnType: typeof(Color),
                declaringType: typeof(GradientShellPage),
                defaultValue: Color.FromHex("#000C72"));

        public static readonly BindableProperty BottomTabBarBottomColorProperty =
        BindableProperty.Create(propertyName: nameof(ToolbarBottomColor),
           returnType: typeof(Color),
           declaringType: typeof(GradientShellPage),
           defaultValue: Color.FromHex("#011966"));

        public static readonly BindableProperty BottomTabBarTopColorProperty =
            BindableProperty.Create(propertyName: nameof(ToolbarTopColor),
                returnType: typeof(Color),
                declaringType: typeof(GradientShellPage),
                defaultValue: Color.FromHex("#0033D5"));

        public Color ToolbarBottomColor
        {
            get { return (Color)GetValue(ToolbarBottomColorProperty); }
            set { SetValue(ToolbarBottomColorProperty, value); }
        }

        public Color ToolbarTopColor
        {
            get { return (Color)GetValue(ToolbarTopColorProperty); }
            set { SetValue(ToolbarTopColorProperty, value); }
        }

        public Color BottomTabBarBottomColor
        {
            get { return (Color)GetValue(BottomTabBarBottomColorProperty); }
            set { SetValue(BottomTabBarBottomColorProperty, value); }
        }

        public Color BottomTabBarTopColor
        {
            get { return (Color)GetValue(BottomTabBarTopColorProperty); }
            set { SetValue(BottomTabBarTopColorProperty, value); }
        }
    }
}
