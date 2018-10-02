using Android.Content;
using NewsApp.CustomView;
using Android.Widget;
using NewsApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBarWithoutIcon), typeof(SearchBarWithoutIconAndroid))]
namespace NewsApp.Droid.Renderer
{
    public class SearchBarWithoutIconAndroid : SearchBarRenderer
    {
        public SearchBarWithoutIconAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            SearchView searchView = Control;
            searchView.Iconified = true;
            searchView.SetIconifiedByDefault(false);

            int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            var icon = searchView.FindViewById(searchIconId);
          
            icon.RemoveFromParent();
        }
    }
}