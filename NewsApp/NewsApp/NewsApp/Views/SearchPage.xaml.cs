using System;
using NewsApp.CustomView;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
	    private readonly SearchBarWithoutIcon _newsSearchBar;
	    private NewsViewModel _newsVM;
	    private ToolbarItem _addItem;
	    private ToolbarItem _deleteItem;
	    private NewsPage _page;
        private bool _add = true;

	    private bool Add
	    {
	        get => _add;
	        set
	        {
	            _add = value;
	            AddOrDeleteToolbarItem();
	        }
	    }

        public SearchPage ()
		{
			InitializeComponent ();

		    NewsView.IsState = State.Normal;
            _newsSearchBar = new SearchBarWithoutIcon()
            {
                Placeholder = Constants.SearhBarPlaceholderText
            };
		    _newsSearchBar.SearchButtonPressed += SearchBar_OnSearchButtonPressed;

		    var searchBarContentView = new ContentView()
		    {
                Content = _newsSearchBar
		    };
            NavigationPage.SetTitleView(this, searchBarContentView);

            SetItems();
		}

	    private async void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
	    {
	        _newsVM = new NewsViewModel(_newsSearchBar.Text)
	        {
                IsState = State.Normal
	        };
            _page = new NewsPage(_newsVM, true);
            NewsView.SetBinding(_newsVM);

	        _newsVM.IsState = State.Loading;
	        await _newsVM.GetNews();            
	    }

	    private void SetItems()
	    {
	        _addItem = new ToolbarItem()
	        {
	            Icon = Constants.Images.Add,
	        };
	        _addItem.Clicked += (sender, args) =>
	        {
	            if (_page != null)
	            {
	                App.NewsPages.Add(_page);
	                Add = !Add;
                }
	        };

	        _deleteItem = new ToolbarItem()
	        {
	            Icon = Constants.Images.Delete
	        };
	        _deleteItem.Clicked += (sender, args) =>
	        {
	            if (_page != null)
	            {
	                App.NewsPages.Remove(_page);
	                Add = !Add;
	            }
	        };

	        ToolbarItems.Add(_add ? _addItem : _deleteItem);
	    }

	    private void AddOrDeleteToolbarItem()
	    {

	        if (_add)
	        {
	            ToolbarItems[0] = _addItem;
	        }
	        else
	        {
	            ToolbarItems[0] = _deleteItem;
	        }
	    }
    }
}