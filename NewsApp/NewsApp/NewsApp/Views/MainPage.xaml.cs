using System;
using Xamarin.Forms;

namespace NewsApp.Views
{
    /// <summary>
    /// Page that contains tab.
    /// </summary>
    public partial class MainPage : TabbedPage
    {
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

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await App.Navigation.PushAsync(new SearchPage());
        }
    }
}
