using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NewsApp.DAL.Models;
using NewsApp.ViewModels;
using NewsApp.Views;
using Plugin.Connectivity;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace NewsApp.CustomView
{
    /// <summary>
    /// View for displaying news.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : ContentView
    {
        private bool _loadingNews;
        private readonly DataTemplate _footer;
        private const int CountNews = (Constants.CountNews.CountPages - 1) * Constants.CountNews.CountArticlesOnPage;
        /// <summary>
        /// List of news.
        /// </summary>
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(ObservableCollection<Article>), typeof(NewsView), null, BindingMode.TwoWay);

        /// <summary>
        /// Page state.
        /// </summary>
        public static readonly BindableProperty IsStateProperty = BindableProperty.Create(nameof(IsState), typeof(State), typeof(NewsView), State.Loading, BindingMode.TwoWay);

        /// <summary>
        /// List of news.
        /// </summary>
        public ObservableCollection<Article> NewsResult
        {
            get => (ObservableCollection<Article>) GetValue(NewsResultProperty);
            set
            {
                SetValue(NewsResultProperty, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Page state.
        /// </summary>
        public State IsState
        {
            get => (State) GetValue(IsStateProperty);
            set
            {
                SetValue(IsStateProperty, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Func for uploading news.
        /// </summary>
        public Func<Task> GetNews { get; set; }

        /// <summary>
        /// Function for adding news when user reached end of news list.
        /// </summary>
        public Func<Task> AddNews { get; set; }

        public NewsView ()
		{
			InitializeComponent ();

		    SetEventHandlers();

		    _footer = NewsListView.FooterTemplate;

		    BindingContext = this;
		}

        /// <summary>
        /// Sets binding with view.
        /// </summary>
        /// <param name="newsVm">View model for news.</param>
        public void SetBinding(NewsViewModel newsVm)
        {
            SetBinding(NewsResultProperty, new Binding() { Source = newsVm, Path = nameof(newsVm.NewsArticles) });
            SetBinding(IsStateProperty, new Binding() { Source = newsVm, Path = nameof(newsVm.IsState) });
            GetNews = newsVm.GetNews;
            AddNews = newsVm.AddNews;
        }

        private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
	    {
	        string url = (itemTappedEventArgs.ItemData as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            IsState = State.Loading;
            await GetNews();
        }

        private void SetEventHandlers()
        {
            PullToRefresh.Refreshing += async (sender, args) =>
            {
                await GetNews();
                NewsListView.FooterTemplate = _footer;
                PullToRefresh.IsRefreshing = false;
            };

            NewsListView.ScrollStateChanged += NewsListViewOnScrollStateChanged; 
        }

        private async void NewsListViewOnScrollStateChanged(object sender, ScrollStateChangedEventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (!_loadingNews)
                {
                    if (NewsResult.Count > CountNews)
                    {
                        return;
                    }

                    var lastIndex = NewsListView.GetVisualContainer().ScrollRows.LastBodyVisibleLineIndex;
                    var header = (NewsListView.HeaderTemplate != null && !NewsListView.IsStickyHeader) ? 1 : 0;
                    var footer = (NewsListView.FooterTemplate != null && !NewsListView.IsStickyFooter) ? 1 : 0;
                    var totalItems = NewsListView.DataSource.DisplayItems.Count + header + footer;

                    if (lastIndex == totalItems - 1)
                    {
                        _loadingNews = true;
                        await AddNews();
                        if (NewsResult.Count > CountNews)
                        {
                            NewsListView.FooterTemplate = null;
                        }
                    }

                    _loadingNews = false;
                }
            }
        }
    }
}