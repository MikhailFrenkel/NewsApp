using NewsApp.DAL.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NewsApp
{
    public partial class App : Application
    {
        private static ArticleRepository _database;
        private MainPage _mainPage;

        public MainPage NewsMainPage
        {
            get => _mainPage;
        }

        public static ArticleRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new ArticleRepository(Constants.DatabaseName);
                }

                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
            _mainPage = new MainPage();
            MainPage = new NavigationPage(_mainPage) { Title = ""}; 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            //_mainPage.OnSleep();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
