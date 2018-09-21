using Android.Content;
using Android.Support.Design.Widget;
using NewsApp;
using NewsApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ScrollableTabbedPageRenderer))]
namespace NewsApp.Droid
{
    /// <summary>
    /// Custom tabbed page.
    /// </summary>
    public class ScrollableTabbedPageRenderer : TabbedPageRenderer
    {
        public ScrollableTabbedPageRenderer(Context context) : base(context)
        {
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child is TabLayout tabLayout)
            {
                tabLayout.TabMode = TabLayout.ModeScrollable;            
            }
        }
    }
}