using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardSelectionPage : ContentPage
    {
        public SelectedStandardsPageViewModel ViewModel = new SelectedStandardsPageViewModel();

        public StandardSelectionPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
            selectedStandardsCollectionView.ItemsSource = ViewModel.Standards;
        }

        private void buttonViewMore_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardDetailPage());
        }

        private void buttonAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new GradientNavigationPage(new StandardCategorisedPage()));
        }
    }
}