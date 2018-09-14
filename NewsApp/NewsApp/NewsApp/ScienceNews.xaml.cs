using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	public partial class ScienceNews : ContentPage
	{
	    public ArticlesResult ScienceNewsResult { get; private set; }

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

	    public ScienceNews()
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
	        ScienceNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
	        {
	            Language = Languages.EN,
                Category = Categories.Science,
	            PageSize = 10
	        });

	        if (ScienceNewsResult.Status == Statuses.Ok)
	        {
	            NewsListView.ItemsSource = ScienceNewsResult.Articles;
	        }
	    }

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }
    }
}