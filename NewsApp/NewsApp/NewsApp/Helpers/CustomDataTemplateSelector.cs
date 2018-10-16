﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsApp.DAL.Models;
using NewsApp.Views.CustomCells;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace NewsApp.Helpers
{
    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _firstArticleDataTemplate;
        private readonly DataTemplate _articleDataTemplate;

        public CustomDataTemplateSelector()
        {
            _firstArticleDataTemplate = new DataTemplate(typeof(FirstArticleViewCell));
            _articleDataTemplate = new DataTemplate(typeof(ArticleViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var listView = container as SfListView;
            var firstItem = listView?.DataSource.Source.ToList<Article>().FirstOrDefault();
            if (firstItem != null && firstItem == item)
            {
                return _firstArticleDataTemplate;
            }

            return _articleDataTemplate;
        }
    }
}
