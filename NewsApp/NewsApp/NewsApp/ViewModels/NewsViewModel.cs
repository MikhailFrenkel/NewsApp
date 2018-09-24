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
        private State _state;
        private readonly string _searchQuery;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public NewsViewModel(string searchQuery)
        {
            _searchQuery = searchQuery;
            _state = State.Loading;
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
            if (NewsArticles == null)
            {
                var res = App.Database.GetItems(_searchQuery);
                if (res.Count() != 0)
                {
                    NewsArticles = (List<Article>) res;
                    IsState = State.Normal;
                    return;
                }
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                var result = await _newsClient.GetNewsAsync(_searchQuery);
                result.Value.Sort(CompareByDatePublished);
                var resultValue = result.Value;
                List<Article> articles = FromValueToArticle(ref resultValue);
                if (NewsArticles == null)
                {
                    NewsArticles = articles;
                }
                else if (NewsArticles.SequenceEqual(articles))
                {
                    NewsArticles = articles;
                }

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
        public void OnSleep()
        {
            //TODO: не успевает сохранить. OnExit?
            foreach (var article in NewsArticles)
            {
                App.Database.SaveItem(article);
            }
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
                    ImageUrl = item.Image?.Thumbnail?.ContentUrl,
                    Description = item.Description,
                    Category = _searchQuery,
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
