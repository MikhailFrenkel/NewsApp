using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.ViewModels;
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
	    private RussianNewsViewModel _russianNewsViewModels;

	    public RussianNews()
	    {
	        InitializeComponent();
	        _russianNewsViewModels = new RussianNewsViewModel();
	        BindingContext = _russianNewsViewModels;
	    }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (_russianNewsViewModels.RussianNewsArticles == null)
	            await _russianNewsViewModels.GetNews();
	    }
	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

	    private async void NewsListView_OnRefreshing(object sender, EventArgs e)
	    {
	        if (_russianNewsViewModels != null)
	            await _russianNewsViewModels.GetNews();
	        NewsListView.IsRefreshing = false;
	    }

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        if (_russianNewsViewModels != null)
	        {
	            _russianNewsViewModels.IsState = State.Loading;
	            await _russianNewsViewModels.GetNews();
	        }
	    }
    }
}