using System;
using NewsApp.ViewModels;
using NewsAPI.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorldNews : ContentPage
    {
        private WorldNewsViewModel _worldNewsViewModels;

        public WorldNews()
        {
            InitializeComponent();
            _worldNewsViewModels = new WorldNewsViewModel();
            WorldNewsView.SetBinding(NewsView.NewsResultProperty, new Binding() { Source = _worldNewsViewModels, Path = "WorldNewsArticles" });
            WorldNewsView.SetBinding(NewsView.IsStateProperty, new Binding() { Source = _worldNewsViewModels, Path = "IsState" });
            //WorldNewsView.NewsResult = _worldNewsViewModels.WorldNewsArticles;
            //WorldNewsView.IsState = _worldNewsViewModels.IsState;
            WorldNewsView.GetNews = _worldNewsViewModels.GetNews;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_worldNewsViewModels.WorldNewsArticles == null)
                await _worldNewsViewModels.GetNews();
        }
    }
}