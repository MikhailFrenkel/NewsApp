using System;
using Plugin.Share;
using Plugin.Share.Abstractions;
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
        /// Web-page url.
        /// </summary>
	    public string Url { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Web-page url.</param>
	    public BrowserPage (string url)
		{
			InitializeComponent ();

		    Url = url;

		    WebView.Source = Url;
		}

        protected override void OnAppearing()
        {
            LoadingIndicator.FadeTo(0.2, 2200, Easing.SpringIn);
        }

        private void WebOnNavigated(object sender, WebNavigatedEventArgs e)
	    {
	        LoadingIndicator.IsRunning = false;
	    }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new ShareMessage()
            {
                Url = Url
            });
        }
    }
}