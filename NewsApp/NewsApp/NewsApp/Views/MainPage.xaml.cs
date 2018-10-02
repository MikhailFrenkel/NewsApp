using System;
using Xamarin.Forms;

namespace NewsApp.Views
{
    /// <summary>
    /// Page that contains tab.
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        public Action SearchButtonClicked;

        public MainPage()
        {
            InitializeComponent();

            if (App.NewsPages != null)
            {
                foreach (var page in App.NewsPages)
                {
                    Children.Add(page);
                }
            }
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            SearchButtonClicked?.Invoke();
            //await Navigation.PushAsync(new SearchPage());
        }
    }
}
