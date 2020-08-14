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
    public partial class StandardDetailPage : ContentPage
    {
        AssessmentStandard Standard { get; set; }

        public StandardDetailPage(AssessmentStandard standard = null)
        {
            InitializeComponent();
            BindingContext = standard;
            Standard = standard;
        }

        private void buttonAccess_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardWebPage(Standard));
        }
    }
}