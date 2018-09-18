using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;

namespace NewsApp.ViewModels
{
    public class RussianNewsViewModel : INotifyPropertyChanged
    {
        private const string Key = "432f183736024ac4aa97b1975eb468ef";
        private readonly NewsApiClient _newsApiClient;
        private List<Article> _russianNewsArticles;
        private State _state;

        public State IsState
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("IsState");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Article> RussianNewsArticles
        {
            get => _russianNewsArticles;
            set
            {
                if (_russianNewsArticles != value)
                {
                    _russianNewsArticles = value;
                    OnPropertyChanged("RussianNewsArticles");
                }
            }
        }

        public RussianNewsViewModel()
        {
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
                //todo: api get news
                var result = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Q = "russia",
                    Language = Languages.EN,
                    PageSize = 10
                });

                if (result.Status == Statuses.Ok)
                {
                    if (RussianNewsArticles == null)
                    {
                        RussianNewsArticles = result.Articles;
                    }
                    else if (RussianNewsArticles.SequenceEqual(result.Articles))
                    {
                        RussianNewsArticles = result.Articles;
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
                if (RussianNewsArticles == null)
                    IsState = State.NoInternet;
            }
        }
    }
}
