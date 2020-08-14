using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardWebPage : ContentPage
    {
        public StandardWebPage(AssessmentStandard standard)
        {
            InitializeComponent();
            webView.BindingContext = standard;
        }
    }
}