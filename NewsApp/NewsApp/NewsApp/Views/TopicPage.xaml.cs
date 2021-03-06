﻿using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopicPage : ContentPage
	{
	    private readonly NewsViewModel _newsVM;
        private readonly NewsPage _page;
	    private ToolbarItem _addItem;
	    private ToolbarItem _deleteItem;
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

        /// <summary>
        /// Initialize topic page.
        /// </summary>
        /// <param name="topic">Topic of your news.</param>
        /// <param name="page">Page that holds this topic.</param>
	    public TopicPage (string topic, NewsPage page = null)
		{
			InitializeComponent ();

		    Title = topic;

		    _newsVM = new NewsViewModel(topic);
            
            NewsView.SetBinding(_newsVM);

		    if (page == null)
		    {
		        _page = new NewsPage(_newsVM, true);
		    }
		    else
		    {
		        _page = page;
		        _add = false;
		    }

            SetItems();
		}

        /// <summary>
        /// Uploads news.
        /// </summary>
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (_newsVM != null)
	        {
	            if (_newsVM.NewsArticles == null)
	                await _newsVM.GetNews();
            }
	    }

        private void SetItems()
	    {
            _addItem = new ToolbarItem()
            {
                Icon = Constants.Images.Add,
            };
	        _addItem.Clicked += (sender, args) =>
	        {
	            App.NewsPages.Add(_page);
	            Add = !Add;
	        };

            _deleteItem = new ToolbarItem()
            {
                Icon = Constants.Images.Delete
            };
	        _deleteItem.Clicked += (sender, args) =>
	        {
	            App.NewsPages.Remove(_page);
	            Add = !Add;
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