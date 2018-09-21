using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
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
	    public string Url { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Web-page url.</param>
	    public BrowserPage (string url)
		{
			InitializeComponent ();
		    Url = url;
            webView.BindingContext = this;
		    webView.SetBinding(WebView.SourceProperty, nameof(Url));
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        await ProgressBarBrowser.ProgressTo(0.9, 1800, Easing.SpringIn);
	    }

	    private void WebOnNavigating(object sender, WebNavigatingEventArgs e)
	    {
	        ProgressBarBrowser.IsVisible = true;
	    }

	    private void WebOnNavigated(object sender, WebNavigatedEventArgs e)
	    {
	        ProgressBarBrowser.IsVisible = false;
	    }

	    private void ToolbarItem_OnActivated(object sender, EventArgs e)
	    {
	        CrossShare.Current.ShareLink(Url);
        }
	}
}