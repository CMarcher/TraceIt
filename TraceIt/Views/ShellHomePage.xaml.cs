using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Views.Charts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellHomePage : GradientShellPage
    {
        public ShellHomePage()
        {
            var tmr = Stopwatch.StartNew();
            InitializeComponent();
            tmr.Stop();
            Debug.WriteLine("Shell took " + tmr.ElapsedMilliseconds + "milliseconds");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }

    }
}