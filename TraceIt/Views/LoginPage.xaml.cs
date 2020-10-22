using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Utilities;
using TraceIt.Views;
using Xamarin.Forms;

namespace TraceIt
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            if (InputFieldsInvalid())
                await DisplayAlert("There's a problem!", "Please fill the required fields.", "Fine");
            else
            {
                CreateUser();
                StatusTracker.CurrentYear = int.Parse((string)pickerLevel.SelectedItem);
                await Navigation.PushModalAsync(new NavigationPage(new SubjectSelectionPage()));
            }
        }

        private bool InputFieldsInvalid()
        {
            if (entryName.Text == "" || pickerLevel.SelectedIndex == -1)
                return true;
            else
                return false;
        }

        private void CreateUser()
        {
            var usermanager = App.UserManagerService;
            var username = entryName.Text;

            usermanager.SetUsername(username);
            usermanager.SetLoginStatus(true);
        }
    }
}
