using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using NewsApp.CustomView;
using NewsApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(HiddenTabsTabbedPage), typeof(HiddenTabsTabbedPageRenderer))]
namespace NewsApp.Droid.Renderer
{
    public class HiddenTabsTabbedPageRenderer : TabbedPageRenderer
    { 
        public HiddenTabsTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            
            for (int i = 0; i < ChildCount; ++i)
            {
                Android.Views.View view = GetChildAt(i);
                if (view is TabLayout tabs)
                {
                    tabs.Visibility = ViewStates.Gone;
                }
            }
        }
    }
}