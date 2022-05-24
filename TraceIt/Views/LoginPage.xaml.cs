using Forms9Patch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Controls;
using TraceIt.Extensions;
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
            TrimUsername();

            if (AFieldIsEmpty())
                await DisplayAlert("There's a problem!", "Please fill the required fields.", "Fine");
            else
            {
                CreateUser();
                StatusTracker.CurrentYear = int.Parse((string)pickerLevel.SelectedItem);

                buttonConfirm.SetEnabledForAndroid(false);
                await Navigation.PushModalAsync(new NavigationPage(new SubjectSelectionPage()));
                buttonConfirm.SetEnabledForAndroid(true);
            }
        }

        private bool AFieldIsEmpty()
        {
            bool usernameEmpty = entryName.Text == "" || entryName.Text is null;
            bool pickerItemNotChosen = pickerLevel.SelectedIndex == -1;

            if (usernameEmpty || pickerItemNotChosen)
                return true;
            else
                return false;
        }

        private void TrimUsername()
            => entryName.Text = entryName.Text?.Trim();

        private void CreateUser()
        {
            var usermanager = App.UserManagerService;
            var username = entryName.Text;

            usermanager.SetUsername(username);
        }
    }
}
