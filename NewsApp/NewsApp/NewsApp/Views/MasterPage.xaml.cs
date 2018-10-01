using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
	    private NavigationPage _newsNavigationPage;
	    private MainPage _mainPage;
	    public MasterPage (MainPage page)
		{
		    InitializeComponent ();
		    TopicsListView.ItemsSource = Constants.Topics;
		    _mainPage = page;
		    _newsNavigationPage = new NavigationPage(_mainPage);
		    Detail = _newsNavigationPage;

		    NavigationPage.SetHasNavigationBar(this, false);}

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        TopicsListView.IsVisible = !TopicsListView.IsVisible;
	    }

	    private async void TopicsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string topic = e.Item as string;
	        await Navigation.PushAsync(new TopicPage(topic)
	        {
	            OnClickedAction = ChangePage
            });
	    }

	    private void ChangePage(string topic)
	    {
	        /*var topics = (List<string>)TopicsListView.ItemsSource;
	        if ((topics).Contains(topic))
	        {
	            topics.Remove(topic);
	        }
	        else
	        {
	            topics.Add(topic);
	        }
            
	        TopicsListView.ItemsSource = topics;*/
	        _mainPage = new MainPage();
            _newsNavigationPage = new NavigationPage(_mainPage);
	        Detail = _newsNavigationPage;
	    }
	}
}