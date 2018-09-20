using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
	    private readonly NewsViewModel _newsViewModel;
		public NewsPage (string title, string searchQuery)
		{
			InitializeComponent ();
		    Title = title;
            _newsViewModel = new NewsViewModel(searchQuery);
		    NewsView.SetBinding(NewsView.NewsResultProperty, new Binding() { Source = _newsViewModel, Path = "NewsArticles" });
		    NewsView.SetBinding(NewsView.IsStateProperty, new Binding() { Source = _newsViewModel, Path = "IsState" });
		    NewsView.GetNews = _newsViewModel.GetNews;
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (_newsViewModel.NewsArticles == null)
	            await _newsViewModel.GetNews();
	    }
    }
}