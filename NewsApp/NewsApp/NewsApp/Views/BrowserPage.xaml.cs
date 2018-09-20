using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowserPage : ContentPage
	{
	    public string Url { get; private set; }

	    public BrowserPage (string url)
		{
			InitializeComponent ();
		    Url = url;
            webView.BindingContext = this;
		    webView.SetBinding(WebView.SourceProperty, "Url");
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
	}
}