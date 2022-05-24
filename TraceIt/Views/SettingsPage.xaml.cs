using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Services;
using TraceIt.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (FieldIsEmpty())
                await DisplayAlert("There's a problem!", "Please enter your name properly", "Fine");
            else
            {
                SaveUser();
                await DisplayAlert("Success!", "Name saved", "Great!");
            }
        }

        private bool FieldIsEmpty()
        {
            bool usernameEmpty = entryName.Text == "" || entryName.Text is null;

            if (usernameEmpty)
                return true;
            else
                return false;
        }

        private void SaveUser()
        {
            var usermanager = App.UserManagerService;
            var username = entryName.Text;

            usermanager.SetUsername(username);
        }

        private void clearCacheButton_Clicked(object sender, EventArgs e)
        {
            CacheManagerService.ClearWebCache();
        }
    }
}