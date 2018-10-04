using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewsApp.ViewModels
{
    public class EditNewsViewModel : INotifyPropertyChanged
    {
        private State _state = State.NoItem;
        private ObservableCollection<string> _newsTitle;

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
        /// List of topics.
        /// </summary>
        public ObservableCollection<string> NewsTitle
        {
            get => _newsTitle;
            set => _newsTitle = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditNewsViewModel()
        { 
            SetNewsTitle();
            _newsTitle.CollectionChanged += (sender, args) =>
            {
                if (_newsTitle.Count == 0)
                {
                    IsState = State.NoItem;
                }
            };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetNewsTitle()
        {
            _newsTitle = new ObservableCollection<string>();
            foreach (var page in App.NewsPages)
            {
                if (page.IsUser)
                {
                    _newsTitle.Add(page.Title);
                }
            }

            if (_newsTitle.Count != 0)
                IsState = State.Normal;
        }
    }
}
