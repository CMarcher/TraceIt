using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views.Charts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherChartsPage : ContentView
    {
        SfPopupLayout popup;
        DataTemplate popupContent;
        CollectionView applicableDegreesCollection;

        public OtherChartsPage()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            CreatePopupLayout();
        }

        private void CreatePopupLayout()
        {
            popup = new SfPopupLayout();
            popup.StaysOpen = true;

            var popupView = popup.PopupView;
            popupView.ShowFooter = false;
            popupView.ShowHeader = true;
            popupView.ShowCloseButton = true;
            popupView.Margin = new Thickness(20, 20);

            applicableDegreesCollection = new CollectionView();
            SetApplicableDegrees();
            popupView.ContentTemplate = CreatePopupContent();
        }

        private void SetApplicableDegrees()
        {
            applicableDegreesCollection.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, new Binding(" ."));
                label.TextColor = Color.Black;

                return label;
            });
        }

        private DataTemplate CreatePopupContent()
        {
            var warningLabel = new Label()
            {
                Text = "* Degrees subject to additional requirements or conditions. " +
                       "This list applies to University of Auckland degrees only.",
                FontAttributes = FontAttributes.Italic
            };

            popupContent = new DataTemplate(() => 
            {
                var baseLayout = new StackLayout();
                baseLayout.Children.Add(warningLabel);
                baseLayout.Children.Add(applicableDegreesCollection);
                baseLayout.Padding = new Thickness(15, 10, 15, 0);
                return baseLayout;
            });

            return popupContent;
        }

        private void SfStepProgressBar_StepTapped(object sender, Syncfusion.XForms.ProgressBar.StepTappedEventArgs e)
        {
            var indicator = e.Item.BindingContext as RankScoreProgressIndicator;
            var degreeList = indicator.DegreeList;
            applicableDegreesCollection.ItemsSource = degreeList;

            CreatePopupContent();
            popup.PopupView.HeaderTitle = "Degrees that require a rank score of " + indicator.Progress;
            popup.PopupView.HeaderHeight = 50;
            popup.Show();
        }
    }
}