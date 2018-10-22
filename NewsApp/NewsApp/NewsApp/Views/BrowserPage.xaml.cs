using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
    /// <summary>
    /// Used for display internet page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserPage : ContentPage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Web-page url.</param>
        public BrowserPage(string url)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new BrowserViewModel(url);
        }

        protected override void OnAppearing()
        {
            LoadingIndicator.FadeTo(0.2, 2200, Easing.SpringIn);
        }

        private void WebOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}