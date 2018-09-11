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
		    BindingContext = this;
		}
	}
}