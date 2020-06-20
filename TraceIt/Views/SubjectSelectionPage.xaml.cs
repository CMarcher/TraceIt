using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectSelectionPage : ContentPage
    {
        public SubjectSelectionPageViewModel datathingy;
        public SubjectSelectionPage()
        {
            InitializeComponent();
            datathingy = new SubjectSelectionPageViewModel();
            BindingContext = datathingy;
            subjectsListView.ItemsSource = datathingy.subjects;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayPromptAsync("New subject", "Enter subject name", "Ok", "Never mind", "Subject");
        }

        private void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubjectsPage());
        }
    }
}