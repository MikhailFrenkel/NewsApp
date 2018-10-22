using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowserPages : CarouselPage
    {
		public BrowserPages (List<string> urls)
		{
			InitializeComponent ();

		    foreach (var url in urls)
		    {
		        Children.Add(new BrowserPage(url));
		    }
		}
    }
}