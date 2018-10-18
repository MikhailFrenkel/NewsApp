using System.Linq;
using System.Threading.Tasks;
using NewsApp.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views
{
    /// <summary>
    /// Page where user can edit own pages.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditNewsPage : ContentPage
	{
	    private readonly EditNewsViewModel _editVM;

	    public EditNewsPage ()
		{
			InitializeComponent ();
		    
            _editVM = new EditNewsViewModel();
		    BindingContext = _editVM;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        NewsTitleListView.ItemDragging += ListView_ItemDragging;
        }

	    protected override void OnDisappearing()
	    {
	        NewsTitleListView.ItemDragging -= ListView_ItemDragging;
            base.OnDisappearing();
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
	                Trash.Source = Constants.Images.TrashBinRed;
	            }
	            else
	            {
	                DeleteLabel.TextColor = Color.Black;
	                Trash.Source = Constants.Images.TrashBin;
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