using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Utilities;
using TraceIt.Services;
using TraceIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradeSelectionPage : ContentPage
    {
        public GradeSelectionPage()
        {
            InitializeComponent();
            Title = "Select grades for " + StandardName;
        }

        private async void closeToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            App.MessagingService.Send(MessagingService.MessageType.PushStandard);
        }

        public string StandardName
        {
            get
            {
                var standard = StatusTracker.CurrentStandard;

                if (standard.StandardType == Models.Standard.StandardTypes.Achievement)
                    return standard.SubjectReference;
                else
                    return "US" + standard.Code.ToString();
            }
        }
    }
}