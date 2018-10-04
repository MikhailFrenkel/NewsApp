using System;
using System.Linq;
using Xamarin.Forms;

namespace NewsApp.Views
{
    /// <summary>
    /// Page that contains tab.
    /// </summary>
    public partial class DetailPage : TabbedPage
    {
        /// <summary>
        /// Main application page.
        /// </summary>
        public DetailPage()
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

        /// <summary>
        /// Uploads tabs.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (App.NewsPages != null)
            {
                if (!Children.SequenceEqual(App.NewsPages))
                {
                    Children.Clear();
                    foreach (var page in App.NewsPages)
                    {
                        Children.Add(page);
                    }
                }
            }
        }

        //TODO: new SearchPage?
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
