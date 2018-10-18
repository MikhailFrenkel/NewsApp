using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using NewsApp.Annotations;
using NewsApp.DAL.Models;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace NewsApp.ViewModels
{
    public class BrowserViewModel : INotifyPropertyChanged
    {
        //private ObservableCollection<Article> _articles;
        private string _url;

       /* public ObservableCollection<Article> Articles
        {
            get => _articles;
            set
            {
                if (_articles != value)
                {
                    _articles = value;
                    OnPropertyChanged();
                }
            }
        }*/

        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShareCommand => new Command(() =>
        {
            CrossShare.Current.Share(new ShareMessage()
            {
                Url = Url
            });
        });

        public BrowserViewModel(string url)
        {
            _url = url;     
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
