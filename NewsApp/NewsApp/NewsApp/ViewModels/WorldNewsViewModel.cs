using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private ArticlesResult _worldNewsResult;
        private State _state;
        public enum State
        {
            Normal,
            Loading,
            Error,
            NoInternet
        }

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

        public ArticlesResult WorldNewsResult
        {
            get => _worldNewsResult;
            set
            {
                if (_worldNewsResult != value)
                {
                    _worldNewsResult = value;
                    OnPropertyChanged("WorldNewsResult");
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
                //todo: api get news
                _worldNewsResult = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Sources = new List<string>() { "google-news" },
                    Language = Languages.EN,
                    PageSize = 10
                });

                if (_worldNewsResult.Status == Statuses.Ok)
                {
                    //NewsListView.ItemsSource = WorldNewsResult.Articles;
                    IsState = State.Normal;
                    OnPropertyChanged("WorldNewsResult");
                }

                if (_worldNewsResult.Status == Statuses.Error)
                {
                    IsState = State.Error;
                }
            }
            else
            {
                IsState = State.NoInternet;
            }
        }
    }
}
