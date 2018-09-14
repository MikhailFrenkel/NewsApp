using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldNews : ContentPage
    {
        /*public State IsState { get; set; } = State.Normal;
        public enum State
        {
            Normal,
            Loading,
            Error,
            NoInternet
        }*/

        public ArticlesResult WorldNewsResult { get; private set; }

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

        private const string Key = "432f183736024ac4aa97b1975eb468ef";
        private readonly NewsApiClient _newsApiClient;

        public WorldNews()
        {
            InitializeComponent();
            BindingContext = this;
            _newsApiClient = new NewsApiClient(Key);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (NewsListView.ItemsSource == null)
                await GetNews();
        }

        private async Task GetNews()
        {
            WorldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Language = Languages.EN,
                PageSize = 10
            });

            if (WorldNewsResult.Status == Statuses.Ok)
            {
                NewsListView.ItemsSource = WorldNewsResult.Articles;
            }
        }

        private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string url = (e.Item as Article)?.Url;
            await Navigation.PushAsync(new BrowserPage(url));
        }
    }
}