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
        
    }
}
