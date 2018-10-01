using System;
using NewsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopicPage : ContentPage
	{
	    public Action<string> OnClickedAction;
	    public NewsViewModel NewsVM;

	    private NewsPage _page;
	    private ToolbarItem _addItem;
	    private ToolbarItem _deleteItem;

	    private bool _add = true;
	    private string _topic;

	    private bool Add
	    {
	        get => _add;
	        set
	        {
	            _add = value;
                AddOrDeleteToolbarItem();
	        }
	    }

	    public TopicPage (string topic, NewsPage page = null)
		{
			InitializeComponent ();

		    _topic = topic;
		    if (page == null)
		    {
		        _page = new NewsPage(topic, topic, true);
            }
		    else
		    {
		        _page = page;
		        _add = false;
		    }
		    
		    Title = topic;
		    SetBindings(topic);
		    SetItems();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        if (NewsVM.NewsArticles == null)
	            await NewsVM.GetNews();
	    }

	    private void SetBindings(string searchQuery)
	    {
	        NewsVM = new NewsViewModel(searchQuery);
	        NewsView.SetBinding(NewsView.NewsResultProperty, new Binding() { Source = NewsVM, Path = nameof(NewsVM.NewsArticles) });
	        NewsView.SetBinding(NewsView.IsStateProperty, new Binding() { Source = NewsVM, Path = nameof(NewsVM.IsState) });
	        NewsView.GetNews = NewsVM.GetNews;
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
	            OnClickedAction?.Invoke(_topic);
	            Add = !Add;
	        };

            _deleteItem = new ToolbarItem()
            {
                Icon = Constants.Images.Delete
            };
	        _deleteItem.Clicked += (sender, args) =>
	        {
	            App.NewsPages.Remove(_page);
	            OnClickedAction?.Invoke(_topic);
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