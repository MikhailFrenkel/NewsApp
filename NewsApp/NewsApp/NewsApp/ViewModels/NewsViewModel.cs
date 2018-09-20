using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;

namespace NewsApp.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        private const string Key = "432f183736024ac4aa97b1975eb468ef";
        private readonly NewsApiClient _newsApiClient;
        private List<Article> _newsArticles;
        private State _state;
        private readonly string _searchQuery;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public NewsViewModel(string searchQuery)
        {
            _searchQuery = searchQuery;
            _state = State.Loading;
            _newsApiClient = new NewsApiClient(Key);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetNews()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var result = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Language = Languages.EN,
                    Q = _searchQuery,
                    PageSize = 10
                });

                if (result.Status == Statuses.Ok)
                {
                    if (NewsArticles == null)
                    {
                        NewsArticles = result.Articles;
                    }
                    else if (NewsArticles.SequenceEqual(result.Articles))
                    {
                        NewsArticles = result.Articles;
                    }

                    IsState = State.Normal;
                }
                else if (result.Status == Statuses.Error)
                {
                    IsState = State.Error;
                }
            }
            else
            {
                if (NewsArticles == null)
                    IsState = State.NoInternet;
            }
        }
    }
}
