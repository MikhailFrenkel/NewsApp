using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NewsApp.DAL.Models;
using Plugin.Connectivity;
using SearchNewsAPI;
using SearchNewsAPI.Models;

namespace NewsApp.ViewModels
{
    /// <summary>
    /// Class that work with bing search news api client.
    /// </summary>
    public class NewsViewModel : INotifyPropertyChanged
    {
        private readonly BingSearchNewsClient _newsClient;
        private List<Article> _newsArticles;
        private State _state = State.Loading;
        private readonly string _searchQuery;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Topic
        {
            get => _searchQuery;
        }

        /// <summary>
        /// Application state.
        /// </summary>
        public State IsState
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// List of articles.
        /// </summary>
        public List<Article> NewsArticles
        {
            get => _newsArticles;
            set
            {
                if (_newsArticles != value)
                {
                    _newsArticles = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Constructor that accept search string.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <param name="articles">List of articles.</param>
        public NewsViewModel(string searchQuery, IEnumerable<Article> articles = null)
        {
            if (articles != null)
            {
                if (articles.Count() != 0)
                {
                    NewsArticles = articles as List<Article>;
                    _state = State.Normal;
                }
            }
            _searchQuery = searchQuery;
            _newsClient = new BingSearchNewsClient(Constants.BingSearchNewsKey);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method for getting news.
        /// </summary>
        /// <returns></returns>
        public async Task GetNews()
        {
            //TODO: проверка на равенство
            if (CrossConnectivity.Current.IsConnected)
            {
                var result = await _newsClient.GetNewsAsync(_searchQuery);
                result.Value.Sort(CompareByDatePublished);
                var resultValue = result.Value;
                List<Article> articles = FromValueToArticle(ref resultValue);
                NewsArticles = articles;

                IsState = State.Normal;
            }
            else
            {
                if (NewsArticles == null)
                    IsState = State.NoInternet;
            }
        }

        /// <summary>
        /// Saves articles to database.
        /// </summary>
        /// <returns>Task.</returns>
        public void OnSleep(string title, bool userPage)
        {
            TopicPage topic = new TopicPage()
            {
                Title = title,
                SearchQuery = _searchQuery,
                UserPage = userPage, 
                Articles = NewsArticles
            };
            App.Database.SaveItem(topic);
        }

        private List<Article> FromValueToArticle(ref List<Value> values)
        {
            var result = new List<Article>();
            foreach (var item in values)
            {
                result.Add(new Article
                {
                    Name = item.Name,
                    DatePublished = item.DatePublished,
                    ImageUrl = item.Image?.Thumbnail?.ContentUrl ?? "kover.jpg",
                    Description = item.Description,
                    Url = item.Url
                });
            }

            return result;
        }

        private int CompareByDatePublished(Value x, Value y)
        {
            if (x.DatePublished > y.DatePublished)
                return -1;

            if (x.DatePublished < y.DatePublished)
                return 1;

            return 0;
        }
    }
}
