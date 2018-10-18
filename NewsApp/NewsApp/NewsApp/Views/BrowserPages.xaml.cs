using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.CustomView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowserPages : HiddenTabsTabbedPage
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