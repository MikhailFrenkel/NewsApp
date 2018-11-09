using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using NewsApp.Interfaces;
using NewsApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo _cultureInfo;

        public string Text { get; set; }

        public TranslateExtension()
        {
            _cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resourceManager = Resource.ResourceManager;

            var translation = resourceManager.GetString(Text, _cultureInfo);

            if (translation == null)
            {
                translation = Text;
            }

            return translation;
        }
    }
}
