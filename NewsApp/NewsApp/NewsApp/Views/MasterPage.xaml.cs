using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
        /// <summary>
        /// Initialize MasterDetailPage.
        /// </summary>
	    public MasterPage ()
		{
		    InitializeComponent ();
		    TopicsListView.ItemsSource = Constants.Topics;
		    Detail = new NavigationPage(new DetailPage());

		    NavigationPage.SetHasNavigationBar(this, false);
		}

        //TODO: new TopicPage?
	    private async void TopicsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string topic = e.Item as string;

	        foreach (var page in App.NewsPages)
	        {
	            if (page.Title == topic)
	            {
	                await Navigation.PushAsync(new TopicPage(topic, page));
	                return;
	            }
	        }

	        await Navigation.PushAsync(new TopicPage(topic));
	    }
	}
}