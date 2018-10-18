using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using NewsApp.DAL.Repositories;
using NewsApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NewsApp
{
    public partial class App : Application
    {
        //TODO: page disposing?
        private static TopicPageRepository _topicPageRepository;

        /// <summary>
        /// Field that contain all news pages.
        /// </summary>
        public static ObservableCollection<NewsPage> NewsPages;

        /// <summary>
        /// Give you access to database.
        /// </summary>
        public static TopicPageRepository Database
        {
            get
            {
                if (_topicPageRepository == null)
                {
                    _topicPageRepository = new TopicPageRepository(Constants.DatabaseName);
                }

                return _topicPageRepository;
            }
        }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionLicenseKey);

            AppCenter.Start("android=6f03a3dd-2147-4af6-82e5-6ed081445fdf;", typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            InitializeNewsPages();

            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Saves news pages in database.
        /// </summary>
        protected override void OnSleep()
        {
            Database.RemoveItems();

            foreach (var page in NewsPages)
            {
                page.OnSleep();
            }
        }

        private void InitializeNewsPages()
        {
            NewsPages = new ObservableCollection<NewsPage>();
            
            var topics = Database.GetItems();
            if (topics != null)
            {
                var topicPages = topics.ToList();
                if (topicPages.Count >= 3)
                {
                    foreach (var topic in topicPages)
                    {
                        NewsPages.Add(new NewsPage(topic.Title, topic.SearchQuery, topic.UserPage, topic.Articles));
                    }

                    return;
                }
            }

            foreach (var topic in Constants.DefaultNews.Topics)
            {
                NewsPages.Add(new NewsPage(topic.Value, topic.Key));
            }
        }
    }
}
