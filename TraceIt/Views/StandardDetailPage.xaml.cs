using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardDetailPage : ContentPage
    {
        public StandardDetailPage()
        {
            InitializeComponent();
        }

        private void buttonAccess_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardWebPage());
        }
    }
}