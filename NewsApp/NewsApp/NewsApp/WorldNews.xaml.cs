using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ListView = Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ListView;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorldNews : ContentPage
	{
        public ArticlesResult WorldNewsResult { get; private set; }

	    private const string Key = "432f183736024ac4aa97b1975eb468ef";
	    private NewsApiClient _newsApiClient;

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
                PageSize = 10,
                Sources = new List<String>() { "bbc-news" }
	        });
           
	        if (WorldNewsResult.Status == Statuses.Ok)
	        {
	            this.BindingContext = this;
	        }
	    }

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
        }
	}
}