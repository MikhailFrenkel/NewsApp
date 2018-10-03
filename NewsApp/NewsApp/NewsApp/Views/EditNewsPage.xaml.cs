using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsApp.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditNewsPage : ContentPage
	{
	    private EditNewsViewModel _editVM;

	    public EditNewsPage ()
		{
			InitializeComponent ();
		    NewsTitleListView.ItemDragging += ListView_ItemDragging;
            _editVM = new EditNewsViewModel();
		    BindingContext = _editVM;
		}

	    private async void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
	    {
            if (e.Action == DragAction.Start)
	        {
	            await Task.Delay(100);
	            HeaderLabel.IsVisible = false;
	            HeaderTrashStackLayout.IsVisible = true;
	        }

	        if (e.Action == DragAction.Dragging)
	        {
                var position = new Point(e.Position.X - NewsTitleListView.Bounds.X, 
                                         e.Position.Y - NewsTitleListView.Bounds.Y);
	            if (Header.Bounds.Contains(position))
	            {
	                DeleteLabel.TextColor = Color.Red;
	            }
	            else
	            {
	                DeleteLabel.TextColor = Color.Black;
	            }
	        }

	        if (e.Action == DragAction.Drop)
	        {
	            var position = new Point(e.Position.X - NewsTitleListView.Bounds.X,
	                e.Position.Y - NewsTitleListView.Bounds.Y);
	            if (Header.Bounds.Contains(position))
	            {
	                await Task.Delay(100);
	                var title = (string)e.ItemData;
	                _editVM.NewsTitle.Remove(title);
	                var page = App.NewsPages.First(x => x.Title == title);
                    if (page != null)
	                    App.NewsPages.Remove(page);
	            }
	            else
	            {
                    App.NewsPages.MoveTo(e.OldIndex + Constants.DefaultNews.Topics.Count, 
                                         e.NewIndex + Constants.DefaultNews.Topics.Count);   
	            }
	            HeaderLabel.IsVisible = true;
	            HeaderTrashStackLayout.IsVisible = false;
            }
	    }
	}
}