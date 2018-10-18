using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.DAL.Models;
using NewsApp.Views.CustomCells;
using Xamarin.Forms;

namespace NewsApp.Helpers
{
    public class BrowserDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _articleDataTemplate;
        private readonly DataTemplate _articleWithoutDataTemplate;

        public BrowserDataTemplateSelector()
        {
            _articleDataTemplate = new DataTemplate(typeof(BrowserArticleViewCell));
            _articleWithoutDataTemplate = new DataTemplate(typeof(BrowserArticleWithoutImageViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return /*(item as Article)?.ImageUrl != null ? _articleDataTemplate :*/ _articleWithoutDataTemplate;
        }
    }
}
