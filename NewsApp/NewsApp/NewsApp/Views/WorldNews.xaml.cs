using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.ViewModels;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldNews : ContentPage
    {
        //todo: viewmodel, service locator

        private WorldNewsViewModel _worldNewsViewModels;

        public WorldNews()
        {
            InitializeComponent();
            _worldNewsViewModels = new WorldNewsViewModel();
            BindingContext = _worldNewsViewModels;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_worldNewsViewModels.WorldNewsArticles == null)
                await _worldNewsViewModels.GetNews();
        }
        private async void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string url = (e.Item as Article)?.Url;
            await Navigation.PushAsync(new BrowserPage(url));
        }

        private async void NewsListView_OnRefreshing(object sender, EventArgs e)
        {
            if (_worldNewsViewModels != null)
                await _worldNewsViewModels.GetNews();
            NewsListView.IsRefreshing = false;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (_worldNewsViewModels != null)
            {
                _worldNewsViewModels.IsState = State.Loading;
                await _worldNewsViewModels.GetNews();
            }
        }
    }
}