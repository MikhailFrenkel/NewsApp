using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Views;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Xamarin.Forms;

namespace NewsApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new NewsPage("World News", "world"));
            Children.Add(new NewsPage("News in Russia", "russia"));
            Children.Add(new NewsPage("Science news", "science"));
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
