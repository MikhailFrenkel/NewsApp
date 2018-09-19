using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BingSearchNewsAPI;
using NewsApp.Models;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScienceNews : ContentPage
	{
	    private const string accessKey = "968875276d614dea8c0f82afbbb6c853";
	    private BingSearchNewsClient _newsClient;
	    private List<Value> _scienceNewsResult;

	    public List<Value> ScienceNewsResult
	    {
	        get => _scienceNewsResult;
	        set
	        {
	            if (_scienceNewsResult != value)
	            {
	                _scienceNewsResult = value;
	                NewsListView.ItemsSource = _scienceNewsResult;
	            }
	        }
	    }

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

	    

	    public ScienceNews()
	    {
	        InitializeComponent();
            _newsClient = new BingSearchNewsClient(accessKey);
	        BindingContext = this;
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
	            var result = await _newsClient.GetNews("Science");
	            ScienceNewsResult = result.Value;
	        }
	    }

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Value)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }
    }
}