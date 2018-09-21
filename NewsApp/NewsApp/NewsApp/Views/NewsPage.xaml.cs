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
    /// <summary>
    /// Page that display list of news.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
	    private readonly NewsViewModel _newsViewModel;
        /// <summary>
        /// Constructor that contains page title and search string.
        /// </summary>
        /// <param name="title">Page title.</param>
        /// <param name="searchQuery">Used for finding news.</param>
		public NewsPage (string title, string searchQuery)
		{
			InitializeComponent ();
		    Title = title;
            _newsViewModel = new NewsViewModel(searchQuery);
		    NewsView.SetBinding(NewsView.NewsResultProperty, new Binding() { Source = _newsViewModel, Path = nameof(_newsViewModel.NewsArticles) });
		    NewsView.SetBinding(NewsView.IsStateProperty, new Binding() { Source = _newsViewModel, Path = nameof(_newsViewModel.IsState) });
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