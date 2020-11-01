using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Services
{
    public static class CacheManagerService
    {
        public static void ClearWebCache()
        {
            DependencyService.Get<ICacheManagerService>().ClearWebCache();
        }
    }
}
