using Android.Graphics.Drawables;
using Google.Android.Material.BottomNavigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TraceIt.Droid.Renderers.Appearance_Trackers
{
    public class GradientShellBottomTabBarAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        IShellContext context;

        public GradientShellBottomTabBarAppearanceTracker(IShellContext context, ShellItem item) : base(context, item)
        {
            this.context = context;
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            var renderer = (GradientShellPageRenderer)context;
            var page = renderer.GetPageInstance();

            var gradient = new GradientDrawable(
                GradientDrawable.Orientation.TopBottom,
                new int[] {
                    page.BottomTabBarTopColor.ToAndroid(),
                    page.BottomTabBarBottomColor.ToAndroid()
                });

            bottomView.SetBackground(gradient);

            
        }
    }
}