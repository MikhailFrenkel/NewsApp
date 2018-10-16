using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views.CustomCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleWithoutImageViewCell : ViewCell
	{
		public ArticleWithoutImageViewCell ()
		{
			InitializeComponent ();
		}
	}
}