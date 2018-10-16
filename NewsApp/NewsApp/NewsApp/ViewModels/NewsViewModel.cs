using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Article> _newsArticles;
        private State _state = State.Loading;
        private int _offset = Constants.CountNews.CountArticlesOnPage;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// News topic.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// Page state.
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
        public ObservableCollection<Article> NewsArticles
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
            if (!CrossConnectivity.Current.IsConnected)
            {
                if (articles != null)
                {
                    var enumerable = articles.ToList();
                    if (enumerable.Count != 0)
                    {
                        ObservableCollection<Article> list = new ObservableCollection<Article>(enumerable);
                        NewsArticles = list;
                        _state = State.Normal;
                    }
                }
            }
            
            Topic = searchQuery;
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
                var result = await _newsClient.GetNewsAsync(Topic);
                if (result != null)
                {
                    result.Value.Sort(CompareByDatePublished);
                    var resultValue = result.Value;
                    ObservableCollection<Article> articles = FromValueToArticle(ref resultValue);
                    NewsArticles = articles;

                    _offset = Constants.CountNews.CountArticlesOnPage;
                    IsState = State.Normal;
                }
                else
                {
                    IsState = State.NoInternet;
                }
            }
            else
            {
                if (NewsArticles == null)
                    IsState = State.NoInternet;
            }
        }

        /// <summary>
        /// Adding news to end of list.
        /// </summary>
        /// <returns></returns>
        public async Task AddNews()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (_offset < Constants.CountNews.CountArticlesOnPage * Constants.CountNews.CountPages)
                {
                    var result = await _newsClient.GetNewsAsync(Topic, _offset);
                    result.Value.Sort(CompareByDatePublished);
                    var resultValue = result.Value;
                    ObservableCollection<Article> articles = FromValueToArticle(ref resultValue);
                    
                    foreach (var article in articles)
                    {
                        NewsArticles.Add(article);
                    }

                    _offset += Constants.CountNews.CountArticlesOnPage;

                    IsState = State.Normal;
                }
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
                SearchQuery = Topic,
                UserPage = userPage, 
                Articles = NewsArticles?.Take(10).ToList()
            };
            App.Database.SaveItem(topic);
        }

        private ObservableCollection<Article> FromValueToArticle(ref List<Value> values)
        {
            var result = new ObservableCollection<Article>();
            foreach (var item in values)
            {
                result.Add(new Article
                {
                    Name = item.Name,
                    DatePublished = item.DatePublished,
                    ImageUrl = item.Image?.ContentUrl ?? "kover.jpg",
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
