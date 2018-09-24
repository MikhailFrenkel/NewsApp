using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApp.Views;
using Xamarin.Forms;

namespace NewsApp
{
    /// <summary>
    /// Page that contains tab.
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        private List<NewsPage> _newsPages;
        public MainPage()
        {
            InitializeComponent();
            InitializeNewsPages();

            foreach (var page in _newsPages)
            {
                Children.Add(page);
            }

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnSleep()
        {
            App.Database.RemoveItems();

            foreach (var page in _newsPages)
            {
                page.NewsVM.OnSleep();
            }
        }

        private void InitializeNewsPages()
        {
            _newsPages = new List<NewsPage>();
            _newsPages.Add(new NewsPage("World News", "world"));
            _newsPages.Add(new NewsPage("News in Russia", "russia"));
            _newsPages.Add(new NewsPage("Science news", "science"));
        }
    }
}
