using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.ViewModels;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : ContentView
    {
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(List<Article>), typeof(NewsView), null);

        public static readonly BindableProperty IsStateProperty = BindableProperty.Create(nameof(IsState), typeof(State), typeof(NewsView), State.Loading, BindingMode.TwoWay);

        public List<Article> NewsResult
        {
            get => (List<Article>) GetValue(NewsResultProperty);
            set
            {
                SetValue(NewsResultProperty, value);
                OnPropertyChanged();
            }
        }

        public State IsState
        {
            get => (State) GetValue(IsStateProperty);
            set
            {
                SetValue(IsStateProperty, value);
                OnPropertyChanged();
            }
        }

        /*private List<Article> _newsResult;
        private State _isState;

        public List<Article> NewsResult
        {
            get => _newsResult;
            set
            {
                if (_newsResult != value)
                {
                    _newsResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public State IsState
        {
            get => _isState;
            set
            {
                if (_isState != value)
                {
                    _isState = value;
                    OnPropertyChanged();
                }
            }
        }*/

        public Func<Task> GetNews { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetNews();
                    NewsListView.IsRefreshing = false;
                });
            }
        }

        public NewsView ()
		{
			InitializeComponent ();
		    BindingContext = this;
		}      

	    private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string url = (e.Item as Article)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            IsState = State.Loading;
            await GetNews();
        }
    }
}