using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TraceIt.Droid
{
    [Activity(Theme = "@style/SplashTheme.Splash", NoHistory = true, 
              MainLauncher = true, Icon = "@mipmap/icon", RoundIcon = "@mipmap/iconround")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}