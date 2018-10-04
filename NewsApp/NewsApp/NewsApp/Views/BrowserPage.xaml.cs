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
        private ToolbarItem _share;
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
            
            _share = new ToolbarItem()
		    {
		        Icon = Constants.Images.Share
		    };
		    ToolbarItems.Add(_share);

		    WebView.Source = Url;
		}

        /// <summary>
        /// Shows activity indicator.
        /// </summary>
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        _share.Clicked += ToolbarItem_OnActivated;
	        await ProgressBarBrowser.ProgressTo(0.9, 1800, Easing.SpringIn);
	    }

	    protected override void OnDisappearing()
	    {
	        _share.Clicked -= ToolbarItem_OnActivated;
	        base.OnDisappearing();
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