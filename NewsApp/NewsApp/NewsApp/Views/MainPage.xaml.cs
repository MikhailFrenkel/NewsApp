using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
        /// <summary>
        /// Initialize MasterDetailPage.
        /// </summary>
	    public MainPage()
		{
		    InitializeComponent ();
            Master = new MasterPage();
		    Detail = new NavigationPage(new DetailPage());
		    NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}