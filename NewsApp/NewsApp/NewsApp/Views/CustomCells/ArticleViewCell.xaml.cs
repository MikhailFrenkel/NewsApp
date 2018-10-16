using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.DAL.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Views.CustomCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleViewCell : ViewCell
	{
		public ArticleViewCell ()
		{
			InitializeComponent();
		}
	}
}