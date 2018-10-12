﻿using System;
using NewsApp.CustomView;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
    /// <summary>
    /// Page for finding news by topic.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
	    private readonly SearchBarWithoutIcon _newsSearchBar;
	    private NewsViewModel _newsVm;
	    private ToolbarItem _addItem;
	    private ToolbarItem _deleteItem;
	    private NewsPage _page;
        private bool _add = true;
	    private State _isState = State.NoItem;

	    private bool Add
	    {
	        get => _add;
	        set
	        {
	            _add = value;
	            AddOrDeleteToolbarItem();
	        }
	    }

	    public State IsState
	    {
	        get => _isState;
	        set
	        {
	            if (_isState != value)
	            {
	                _isState = value;
                    OnPropertyChanged();
	            }
	        }
	    }

	    /// <summary>
        /// Initialize page that contain search bar.
        /// </summary>
        public SearchPage ()
		{
			InitializeComponent ();

		    NewsView.IsState = State.Normal;
            _newsSearchBar = new SearchBarWithoutIcon()
            {
                Placeholder = Constants.SearhBarPlaceholderText
            };

		    var searchBarContentView = new ContentView()
		    {
                Content = _newsSearchBar
		    };
            NavigationPage.SetTitleView(this, searchBarContentView);

            SetItems();

		    BindingContext = this;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        _newsSearchBar.SearchButtonPressed += SearchBar_OnSearchButtonPressed;
        }

	    protected override void OnDisappearing()
	    {
	        base.OnDisappearing();
	        _newsSearchBar.SearchButtonPressed -= SearchBar_OnSearchButtonPressed;
	    }

	    private async void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
	    {
	        IsState = State.Normal;

	        if (!Add)
	            Add = !Add;

	        _newsVm = new NewsViewModel(_newsSearchBar.Text);
            _page = new NewsPage(_newsVm, true);
            NewsView.SetBinding(_newsVm);

	        _newsVm.IsState = State.Loading;
	        await _newsVm.GetNews();            
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