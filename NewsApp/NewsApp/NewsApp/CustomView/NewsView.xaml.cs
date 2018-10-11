using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApp.DAL.Models;
using NewsApp.ViewModels;
using NewsApp.Views;
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
        /// <summary>
        /// List of news.
        /// </summary>
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(List<Article>), typeof(NewsView), null, BindingMode.TwoWay);

        /// <summary>
        /// Page state.
        /// </summary>
        public static readonly BindableProperty IsStateProperty = BindableProperty.Create(nameof(IsState), typeof(State), typeof(NewsView), State.Loading, BindingMode.TwoWay);

        /// <summary>
        /// List of news.
        /// </summary>
        public List<Article> NewsResult
        {
            get => (List<Article>) GetValue(NewsResultProperty);
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

        public NewsView ()
		{
			InitializeComponent ();
		    PullToRefresh.Refreshing += async (sender, args) =>
		    {
		        await GetNews();
		        PullToRefresh.IsRefreshing = false;
		    };
		    BindingContext = this;
		}

        /// <summary>
        /// Sets binding with view.
        /// </summary>
        /// <param name="newsVM">View model for news.</param>
        public void SetBinding(NewsViewModel newsVM)
        {
            SetBinding(NewsResultProperty, new Binding() { Source = newsVM, Path = nameof(newsVM.NewsArticles) });
            SetBinding(IsStateProperty, new Binding() { Source = newsVM, Path = nameof(newsVM.IsState) });
            GetNews = newsVM.GetNews;
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
    }
}