using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public partial class SubjectSelectionPage : ContentPage
    {
        public SubjectSelectionPageViewModel ViewModel;

        public SubjectSelectionPage()
        {
            InitializeComponent();
            ViewModel = new SubjectSelectionPageViewModel();
            BindingContext = ViewModel;
            subjectsListView.ItemsSource = ViewModel.subjects;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayPromptAsync("New subject", "Enter subject name", "Ok", "Never mind", "Subject");
        }

        private void buttonConfirm_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ShellHomePage();
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            DisplayPromptAsync("Add New Subject", "Enter the name of your subject", "OK", "Cancel", "Subject name");
        }
    }
}