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
            
            var share = new ToolbarItem()
		    {
		        Icon = Constants.Images.Share
		    };
		    share.Clicked += ToolbarItem_OnActivated;
		    ToolbarItems.Add(share);

		    WebView.Source = Url;
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
	        CrossShare.Current.Share(new ShareMessage()
	        {
	            Url = Url
	        });
	    }
	}
}