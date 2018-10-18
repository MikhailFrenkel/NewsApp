using NewsApp.Models;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
	    private readonly MasterViewModel _masterVm;
		public MasterPage ()
		{
			InitializeComponent ();
		    _masterVm = new MasterViewModel();
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
	}
}