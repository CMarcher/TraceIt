using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models.Query_Models;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardCategorisedPage : ContentPage
    {
        StandardCategorisedPageViewModel ViewModel;

        public StandardCategorisedPage()
        {
            InitializeComponent();
            ViewModel = new StandardCategorisedPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetItemsSource();
        }

        void SetItemsSource()
        {
            bool unitFilterSelected = assessmentSelector.SelectedIndex == 1;
            if (unitFilterSelected)
                collectionViewCategories.ItemsSource = ViewModel.Subfields;
            else
                collectionViewCategories.ItemsSource = ViewModel.Subjects;
        }

        private void assessmentSelector_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            SetItemsSource();
        }

        private void collectionViewCategories_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {
                var item = (SubfieldModel)e.ItemData;
                Navigation.PushAsync(new StandardCategorisedDetailPage(item.Subfield, Services.DataService.FilterByOption.Subfield));
            }
            catch { DisplayAlert("Error!", "Don't select subject items!", "I'm sorry!"); }
        }

        private void closeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}