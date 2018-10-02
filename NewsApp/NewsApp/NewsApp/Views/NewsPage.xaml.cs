using System.Collections.Generic;
using NewsApp.DAL.Models;
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
        private readonly bool _userNews;
	    private readonly NewsViewModel _newsVM;

        /// <summary>
        /// Constructor that contains page title and search string.
        /// </summary>
        /// <param name="title">Page title.</param>
        /// <param name="searchQuery">Used for finding news.</param>
        /// <param name="userNews">User page.</param>
        /// <param name="articles">List of articles.</param>
        public NewsPage (string title, string searchQuery, bool userNews = false, IEnumerable<Article> articles = null)
		{
			InitializeComponent ();

		    _userNews = userNews;
		    Title = title;
		    if (articles != null)
		    {
		        _newsVM = new NewsViewModel(searchQuery, articles);
		    }
		    else
		    {
		        _newsVM = new NewsViewModel(searchQuery);
		    }

		    NewsView.SetBinding(_newsVM);

		    if (_userNews)
		    {
                var item = new ToolbarItem()
                {
                    Icon = Constants.Images.Edit
                };
                ToolbarItems.Add(item);
		    }
		}

        public NewsPage(NewsViewModel nvm, bool userNews = false)
        {
            InitializeComponent();

            _newsVM = nvm;
            NewsView.SetBinding(_newsVM);
            _userNews = userNews;
            Title = nvm.Topic;

            if (_userNews)
            {
                var item = new ToolbarItem()
                {
                    Icon = Constants.Images.Edit
                };
                ToolbarItems.Add(item);
            }
        }

        /// <summary>
        /// Saves articles to database.
        /// </summary>
        public void OnSleep()
        {
            _newsVM.OnSleep(Title, _userNews);
        }

        /// <summary>
        /// Upload news.
        /// </summary>
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (_newsVM.NewsArticles == null)
	            await _newsVM.GetNews();
	    }
	}
}