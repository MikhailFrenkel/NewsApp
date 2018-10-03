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
        private bool _userNews;
	    private readonly NewsViewModel _newsVM;

        public bool IsUser
        {
            get => _userNews;
        }

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

		    SetUserNews(userNews);
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
		}

        public NewsPage(NewsViewModel nvm, bool userNews = false)
        {
            InitializeComponent();

            _newsVM = nvm;
            NewsView.SetBinding(_newsVM);
            Title = nvm.Topic;
            SetUserNews(userNews);
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

        private void SetUserNews(bool userNews)
        {
            _userNews = userNews;
            if (_userNews)
            {
                var item = new ToolbarItem()
                {
                    Icon = Constants.Images.Edit
                };
                item.Clicked += async (sender, args) => { await App.Navigation.PushAsync(new EditNewsPage()); };
                ToolbarItems.Add(item);
            }
        }
    }
}