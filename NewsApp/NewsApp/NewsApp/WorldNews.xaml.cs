using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldNews : ContentPage
    {
        public State IsState { get; set; } = State.Normal;
        public ArticlesResult WorldNewsResult { get; private set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await RefreshListView();
                    NewsListView.IsRefreshing = false;
                });
            }
        }

        public enum State
        {
            Normal,
            Loading,
            Error,
            NoInternet
        }

        private const string Key = "432f183736024ac4aa97b1975eb468ef";
        private readonly NewsApiClient _newsApiClient;

        public WorldNews()
        {
            InitializeComponent();
            _newsApiClient = new NewsApiClient(Key);
            GetNews();
        }

        private async void GetNews()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                IsState = State.NoInternet;
                return;
            }

            WorldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Language = Languages.EN,
                PageSize = 10
            });

            if (WorldNewsResult.Status == Statuses.Ok)
            {
                BindingContext = this;
                IsState = State.Normal;
            }
            else
            {
                IsState = State.Error;
            }
        }

        private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string url = (e.Item as Article)?.Url;
            await Navigation.PushAsync(new BrowserPage(url));
        }

        private async Task RefreshListView()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                IsState = State.NoInternet;
                return;
            }

            WorldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Language = Languages.EN,
                PageSize = 10
            });

            if (WorldNewsResult.Status == Statuses.Ok)
            {
                NewsListView.ItemsSource = WorldNewsResult.Articles;
                IsState = State.Normal;
            }
            else
            {
                IsState = State.Error;
            }
        }
    }
}