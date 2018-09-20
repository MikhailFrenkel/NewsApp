using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace NewsApp.ViewModels
{
    public class WorldNewsViewModel : INotifyPropertyChanged
    {
        private const string Key = "432f183736024ac4aa97b1975eb468ef";
        private readonly NewsApiClient _newsApiClient;
        private List<Article> _worldNewsArticles;
        private State _state;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Article> WorldNewsArticles
        {
            get => _worldNewsArticles;
            set
            {
                if (_worldNewsArticles != value)
                {
                    _worldNewsArticles = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorldNewsViewModel()
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
                var result = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Sources = new List<string>() { "google-news" },
                    Language = Languages.EN,
                    PageSize = 10
                });

                if (result.Status == Statuses.Ok)
                {
                    if (WorldNewsArticles == null)
                    {
                        WorldNewsArticles = result.Articles;
                    }
                    else if (WorldNewsArticles.SequenceEqual(result.Articles))
                    {
                        WorldNewsArticles = result.Articles;
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
                if (WorldNewsArticles == null)
                    IsState = State.NoInternet;
            }
        }
    }
}
