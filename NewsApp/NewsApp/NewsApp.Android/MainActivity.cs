using Android.App;
using Android.Content.PM;
using Android.OS;

namespace NewsApp.Droid
{
    [Activity(Label = "NewsApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App _app;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            _app = new App();
            LoadApplication(_app);
        }

        protected override void OnDestroy()
        {
            //_app.NewsMainPage.OnSleep();
            base.OnDestroy();
        }

        protected override void OnStop()
        {
            _app.NewsMainPage.OnSleep();
            base.OnStop();
        }

    }
}