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
    public partial class StandardSelectionPage : ContentPage
    {
        public SelectedStandardsPageViewModel ViewModel = new SelectedStandardsPageViewModel();

        public StandardSelectionPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
            selectedStandardsCollectionView.ItemsSource = ViewModel.Standards;
        }
    }
}