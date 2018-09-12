using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorldNews : ContentPage
	{
        public ArticlesResult WorldNewsResult { get; private set; }

	    public ICommand RefreshCommand
	    {
	        get
	        {
                return new Command(async () =>
                {
                    await RefreshListView();
                    NewsListView.IsRefreshing = false;
                });
	        }
	    }


	    private const string Key = "432f183736024ac4aa97b1975eb468ef";
	    private readonly NewsApiClient _newsApiClient;

        public WorldNews ()
		{
			InitializeComponent ();
		    _newsApiClient = new NewsApiClient(Key);
		    GetNews();
		}
        private async void GetNews()
	    {
	        WorldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
	        {
	            Language = Languages.EN,
                Sources = new List<String>() { "bbc-news" }
	        });

            if (WorldNewsResult.Status == Statuses.Ok)
	        {
	            BindingContext = this;
	        }
	    }

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
        }

	    private async Task RefreshListView()
	    {
	        WorldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
	        {
	            Language = Languages.EN,
	            Sources = new List<String>() { "bbc-news" }
	        }); 
            NewsListView.ItemsSource = WorldNewsResult.Articles;
	    }
	}
}