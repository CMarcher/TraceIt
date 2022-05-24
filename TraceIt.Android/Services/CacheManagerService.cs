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
using TraceIt.Droid.Services;
using TraceIt.Services;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(TraceIt.Droid.Services.CacheManagerService))]
namespace TraceIt.Droid.Services
{
    public class CacheManagerService : ICacheManagerService
    {
        public void ClearWebCache()
        {
            try
            {
                var cachePath = Path.GetTempPath();

                if (Directory.Exists(cachePath))
                    Directory.Delete(cachePath);

                if (Directory.Exists(cachePath) is false)
                    Directory.CreateDirectory(cachePath);
            }
            catch(Exception) { }
        }
    }
}