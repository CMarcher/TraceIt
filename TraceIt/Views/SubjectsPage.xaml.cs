using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using TraceIt.Utilities;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectsPage : ContentPage
    {
        SubjectsPageViewModel ViewModel;
        public SubjectsPage()
        {
            InitializeComponent();

            ViewModel = new SubjectsPageViewModel();
            BindingContext = ViewModel;
            collectionViewSubjects.ItemsSource = ViewModel.Subjects;
        }

        private void buttonViewInfo_Clicked(object sender, EventArgs e)
        {
            var subject = (sender as SfButton)?.BindingContext as Subject;
            StatusTracker.CurrentSubject = subject;

            Navigation.PushAsync(new StandardSelectionPage());
        }
    }
}