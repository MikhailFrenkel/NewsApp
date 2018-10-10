using System;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace NewsAppWithMvvmCross.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstPage : MvxContentPage
	{
		public FirstPage ()
		{
			InitializeComponent ();
		}
	}
}