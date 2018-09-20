using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BingSearchNewsAPI;
using NewsApp.Models;
using Plugin.Connectivity;

namespace NewsApp.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        private const string Key = "968875276d614dea8c0f82afbbb6c853";
        private readonly BingSearchNewsClient _newsClient;
        private List<Value> _newsArticles;
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

        public NewsViewModel(string searchQuery)
        {
            _searchQuery = searchQuery;
            _state = State.Loading;
            _newsClient = new BingSearchNewsClient(Key);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetNews()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var result = await _newsClient.GetNews(_searchQuery);
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
