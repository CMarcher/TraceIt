using Plugin.StoreReview;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TraceIt.Services
{
    public static class ReviewManagerService
    {
        public static bool IsWaitingForChance { get; private set; } = false;

        public static async Task RequestReview()
        {
            if (CanRequestReview())
            {
                await CrossStoreReview.Current.RequestReview(true);
                SaveReviewDetails();
            }
        }

        private static bool CanRequestReview()
        {
            string lastReviewedVersion = App.UserManagerService.ReviewedVersion;
            DateTime lastReviewedDate = App.UserManagerService.ReviewedDate;
            double daysFromLastReview = DateTime.Now.Subtract(lastReviewedDate).TotalDays;

            bool currentVersionNotReviewed = lastReviewedVersion != AppInfo.VersionString;
            bool notReviewedInAWhile = lastReviewedDate != default ? daysFromLastReview > 30 : true;
            bool canReview = currentVersionNotReviewed && notReviewedInAWhile;

            return canReview;
        }

        private static void SaveReviewDetails()
        {
            SetVersionForReview();
            SetReviewDate();
        }

        private static void SetVersionForReview()
        {
            string version = AppInfo.VersionString;
            App.UserManagerService.SetReviewedVersion(version);
        }

        private static void SetReviewDate()
            => App.UserManagerService.SetReviewedDate(DateTime.Now);
    }
}
