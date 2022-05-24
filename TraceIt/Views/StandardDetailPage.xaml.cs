using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIt.Extensions;
using TraceIt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardDetailPage : ContentPage
    {
        Standard Standard { get; set; }

        public StandardDetailPage(Standard standard = null)
        {
            InitializeComponent();
            Initialise(standard);
        }

        private void Initialise(Standard standard = null)
        {
            BindingContext = standard;
            Standard = standard;
            SetTitle();
        }

        private void SetTitle()
        {
            string unitTitle = Standard.Code + " Details";
            string achievementTitle = Standard.SubjectReference + " Details";

            Title = Standard.StandardType is Standard.StandardTypes.Unit ? unitTitle : achievementTitle;
        }

        private async void buttonAccess_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            button.SetEnabledForAndroid(false);
            await Navigation.PushAsync(new StandardWebPage(Standard));
            button.SetEnabledForAndroid(true);
        }
    }
}