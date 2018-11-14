using System.Threading.Tasks;
using Microsoft.Identity.Client;
using NewsApp.Models;
using NewsApp.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
	    private readonly MasterViewModel _masterVm;
		public MasterPage (AuthenticationResult ar)
		{
			InitializeComponent ();
		    _masterVm = new MasterViewModel(ShowMessage, ar);
            BindingContext = _masterVm;
        }

	    private async void SfListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        if (e.ItemData is Topic topic)
	        {
	            foreach (var page in App.NewsPages)
	            {
	                if (page.Title == topic.Name)
	                {
	                    await Navigation.PushAsync(new TopicPage(topic.Name, page));
	                    return;
	                }
	            }

	            await Navigation.PushAsync(new TopicPage(topic.Name));
            }
        }

	    private async Task ShowMessage(string title, string message, string cancel)
	    {
	        await DisplayAlert(title, message, cancel);
	    }
	}
}