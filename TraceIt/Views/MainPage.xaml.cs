using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Views;
using Xamarin.Forms;

namespace TraceIt
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            if (InputFieldsInvalid())
            {
                await DisplayAlert("There's a problem!", "Please fill the required fields.", "Fine");
            }
            else
            {
                await Navigation.PushAsync(new SubjectSelectionPage());
            }
        }

        public bool InputFieldsInvalid()
        {
            if (entryName.Text == "" || pickerLevel.SelectedItem == null)
            {
                return true;
            }
            else
                return false;

        }
    }
}
