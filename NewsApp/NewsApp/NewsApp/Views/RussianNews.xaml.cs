using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RussianNews : ContentPage
	{
	    public ArticlesResult RussianNewsResult { get; private set; }

	    public ICommand RefreshCommand
	    {
	        get
	        {
	            return new Command(async () =>
	            {
	                await GetNews();
	                NewsListView.IsRefreshing = false;
	            });
	        }
	    }

	    private const string Key = "432f183736024ac4aa97b1975eb468ef";
	    private readonly NewsApiClient _newsApiClient;

	    public RussianNews()
	    {
	        InitializeComponent();
	        BindingContext = this;
	        _newsApiClient = new NewsApiClient(Key);
	    }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

            if (NewsListView.ItemsSource == null)
	            await GetNews();
	    }

	    private async Task GetNews()
	    {
	        if (CrossConnectivity.Current.IsConnected)
	        {
	            RussianNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
	            {
	                Q = "russia",
	                Language = Languages.EN,
	                PageSize = 10
	            });

	            if (RussianNewsResult.Status == Statuses.Ok)
	            {
	                NewsListView.ItemsSource = RussianNewsResult.Articles;
	            }
	        }
	    }

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }
    }
}