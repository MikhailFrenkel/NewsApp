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
	    private const string Key = "432f183736024ac4aa97b1975eb468ef";
	    private NewsApiClient _newsApiClient;

		public WorldNews ()
		{
			InitializeComponent ();

		    _newsApiClient = new NewsApiClient(Key);
		    var response = _newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
		    {
                Language = Languages.EN
		    });
		}
	}
}