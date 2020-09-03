using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradeSelectionPage : ContentPage
    {
        public GradeSelectionPage()
        {
            InitializeComponent();
            Title = "Select grades for " + StatusTracker.CurrentSubject.Name;
            BindingContext = StatusTracker.CurrentStandard;
        }

        private async void closeToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}