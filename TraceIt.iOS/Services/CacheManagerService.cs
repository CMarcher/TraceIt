using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TraceIt.iOS.Services;
using TraceIt.Services;
using UIKit;
using WebKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TraceIt.iOS.Services.CacheManagerService))]
namespace TraceIt.iOS.Services
{
    public class CacheManagerService : ICacheManagerService
    {
        public void ClearWebCache()
        {
            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            NSUrlCache.SharedCache.DiskCapacity = 0;
            NSUrlCache.SharedCache.MemoryCapacity = 0;
        }
    }
}