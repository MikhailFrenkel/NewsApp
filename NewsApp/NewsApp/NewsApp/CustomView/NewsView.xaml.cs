using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.Models;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : ContentView
    {
        public static readonly BindableProperty NewsResultProperty = BindableProperty.Create(nameof(NewsResult), typeof(List<Value>), typeof(NewsView), null);

        public static readonly BindableProperty IsStateProperty = BindableProperty.Create(nameof(IsState), typeof(State), typeof(NewsView), State.Loading, BindingMode.TwoWay);

        public List<Value> NewsResult
        {
            get => (List<Value>) GetValue(NewsResultProperty);
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
	        string url = (e.Item as Value)?.Url;
	        await Navigation.PushAsync(new BrowserPage(url));
	    }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            IsState = State.Loading;
            await GetNews();
        }
    }
}