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
	    public readonly NewsViewModel NewsVM;
        /// <summary>
        /// Constructor that contains page title and search string.
        /// </summary>
        /// <param name="title">Page title.</param>
        /// <param name="searchQuery">Used for finding news.</param>
		public NewsPage (string title, string searchQuery)
		{
			InitializeComponent ();
		    Title = title;
		    NewsVM = new NewsViewModel(searchQuery);
		    NewsView.SetBinding(NewsView.NewsResultProperty, new Binding() { Source = NewsVM, Path = nameof(NewsVM.NewsArticles) });
		    NewsView.SetBinding(NewsView.IsStateProperty, new Binding() { Source = NewsVM, Path = nameof(NewsVM.IsState) });
		    NewsView.GetNews = NewsVM.GetNews;
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (NewsVM.NewsArticles == null)
	            await NewsVM.GetNews();
	    }

        
    }
}