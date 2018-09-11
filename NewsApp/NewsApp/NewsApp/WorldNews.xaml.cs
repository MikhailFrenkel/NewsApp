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
	            Language = Languages.EN
	        });
           
	        if (WorldNewsResult.Status == Statuses.Ok)
	        {
	            this.BindingContext = this;
	        }
	    }
    }
}