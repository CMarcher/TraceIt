using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
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

            //var model = collectionViewSubjects.ItemsSource.
            //model.IsVisible = true;
        }

        private void buttonViewInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardSelectionPage());
        }
    }
}