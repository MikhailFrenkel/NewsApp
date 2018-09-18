using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    public delegate Task GetNews();

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : ContentView
    {
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(ArticlesResult),
            typeof(NewsView), propertyChanged: NewsResultPropertyChanged);  

        public static readonly BindableProperty RefreshNewsProperty = BindableProperty.Create(nameof(RefreshNews), typeof(GetNews), typeof(NewsView));

        public GetNews RefreshNews
        {
            get => (GetNews) GetValue(RefreshNewsProperty);
            set => SetValue(RefreshNewsProperty, value);
        }

        public ArticlesResult NewsResult
        {
            get => (ArticlesResult)GetValue(NewsResultProperty);
            set => SetValue(NewsResultProperty, value);
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (RefreshNews != null)
                    {
                        await RefreshNews();
                        NewsListView.ItemsSource = NewsResult.Articles;
                    }
                    NewsListView.IsRefreshing = false;
                });
            }
        }

        public static void Init()
        {
        }

        public NewsView ()
		{
			InitializeComponent ();
		}      

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

        private static void NewsResultPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is NewsView nv)
                nv.NewsListView.ItemsSource = (newvalue as ArticlesResult)?.Articles;

        }
    }
}