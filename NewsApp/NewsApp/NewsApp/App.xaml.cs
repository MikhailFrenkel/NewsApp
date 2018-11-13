using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using NewsApp.DAL.Repositories;
using NewsApp.Resources;
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

        public static PublicClientApplication PublicClientApplication { get; private set; }

        public static UIParent UiParent { get; set; }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionLicenseKey);

            AppCenter.Start(Constants.AppCenterLicenseKey, typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            PublicClientApplication = new PublicClientApplication(Constants.B2C.ApplicationId, Constants.B2C.Authority)
            {
                RedirectUri = Constants.B2C.RedirectUrl
            };

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
                    for (int i = 0; i < topicPages.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                NewsPages.Add(new NewsPage(Resource.DefaultTopicWorldNewsText, Resource.DefaultTopicWorldNewsSearchString, 
                                                            topicPages[i].UserPage, topicPages[i].Articles));
                                break;
                            case 1:
                                NewsPages.Add(new NewsPage(Resource.DefaultTopicRussiaNewsText, Resource.DefaultTopicRussiaNewsSearchString,
                                    topicPages[i].UserPage, topicPages[i].Articles));
                                break;
                            case 2:
                                NewsPages.Add(new NewsPage(Resource.DefaultTopicScienceText, Resource.DefaultTopicScienceSearchString,
                                    topicPages[i].UserPage, topicPages[i].Articles));
                                break;
                            default:
                                NewsPages.Add(new NewsPage(topicPages[i].Title, topicPages[i].SearchQuery, topicPages[i].UserPage, topicPages[i].Articles));
                                break;
                        }
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
