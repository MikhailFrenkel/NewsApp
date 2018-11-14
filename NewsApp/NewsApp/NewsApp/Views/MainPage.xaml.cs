using System;
using Microsoft.Identity.Client;
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
	    public MainPage(AuthenticationResult ar)
		{
		    InitializeComponent ();
            Master = new MasterPage(ar);
		    Detail = new NavigationPage(new DetailPage());
		    NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}