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
        private readonly NewsViewModel _newsVm;

        /// <summary>
        /// Returns true if this is user page.
        /// </summary>
        public bool IsUser { get; private set; }

        /// <summary>
        /// Initialize news page.
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
		        _newsVm = new NewsViewModel(searchQuery, articles);
		    }
		    else
		    {
		        _newsVm = new NewsViewModel(searchQuery);
		    }

		    NewsView.SetBinding(_newsVm);
		}

        /// <summary>
        /// Initialize news page.
        /// </summary>
        /// <param name="nvm">View model for news page.</param>
        /// <param name="userNews">True if this is user page.</param>
        public NewsPage(NewsViewModel nvm, bool userNews = false)
        {
            InitializeComponent();

            _newsVm = nvm;
            NewsView.SetBinding(_newsVm);
            Title = nvm.Topic;
            SetUserNews(userNews);
        }

        /// <summary>
        /// Saves articles to database.
        /// </summary>
        public void OnSleep()
        {
            _newsVm.OnSleep(Title, IsUser);
        }

        /// <summary>
        /// Upload news.
        /// </summary>
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (_newsVm.NewsArticles == null)
	            await _newsVm.GetNews();
	    }

        //TODO: new EditPage?
        //TODO: item.Clicked += () => {} ?
        private void SetUserNews(bool userNews)
        {
            IsUser = userNews;
            if (IsUser)
            {
                var item = new ToolbarItem()
                {
                    Icon = Constants.Images.Edit
                };
                item.Clicked += async (sender, args) => { await Navigation.PushAsync(new EditNewsPage()); };
                ToolbarItems.Add(item);
            }
        }
    }
}