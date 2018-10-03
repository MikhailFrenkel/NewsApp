using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
	    private NavigationPage _newsNavigationPage;
	    private MainPage _mainPage;
	    public MasterPage ()
		{
		    InitializeComponent ();
		    TopicsListView.ItemsSource = Constants.Topics;
		    _mainPage = new MainPage();
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

	    public void ChangeDetailPage()
	    {
            _mainPage = new MainPage();
            _newsNavigationPage = new NavigationPage(_mainPage);
	        Detail = _newsNavigationPage;
	    }
	}
}