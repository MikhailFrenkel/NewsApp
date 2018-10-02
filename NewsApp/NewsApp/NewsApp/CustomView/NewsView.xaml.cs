using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.DAL.Models;
using NewsApp.ViewModels;
using NewsApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.CustomView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : ContentView
    {
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(List<Article>), typeof(NewsView), null);

        public static readonly BindableProperty IsStateProperty = BindableProperty.Create(nameof(IsState), typeof(State), typeof(NewsView), State.Loading, BindingMode.TwoWay);

        public List<Article> NewsResult
        {
            get => (List<Article>) GetValue(NewsResultProperty);
            set
            {
                SetValue(NewsResultProperty, value);
                OnPropertyChanged();
            }
        }

        public State IsState
        {
            get => (State) GetValue(IsStateProperty);
            set
            {
                SetValue(IsStateProperty, value);
                OnPropertyChanged();
            }
        }

        public Func<Task> GetNews { get; set; }

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

        public NewsView ()
		{
			InitializeComponent ();
		    BindingContext = this;
		}

        public void SetBinding(NewsViewModel newsVM)
        {
            SetBinding(NewsResultProperty, new Binding() { Source = newsVM, Path = nameof(newsVM.NewsArticles) });
            SetBinding(IsStateProperty, new Binding() { Source = newsVM, Path = nameof(newsVM.IsState) });
            GetNews = newsVM.GetNews;
        }

        private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            IsState = State.Loading;
            await GetNews();
        }
    }
}