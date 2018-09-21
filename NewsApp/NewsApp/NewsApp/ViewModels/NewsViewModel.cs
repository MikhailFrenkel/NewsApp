using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        private List<Value> _newsArticles;
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
        public List<Value> NewsArticles
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
        /// Method for getting news
        /// </summary>
        /// <returns></returns>
        public async Task GetNews()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var result = await _newsClient.GetNewsAsync(_searchQuery);
                result.Value.Sort(CompareByDatePublished);
                if (NewsArticles == null)
                {
                    NewsArticles = result.Value;
                }
                else if (NewsArticles.SequenceEqual(result.Value))
                {
                    NewsArticles = result.Value;
                }

                IsState = State.Normal;
            }
            else
            {
                if (NewsArticles == null)
                    IsState = State.NoInternet;
            }
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
