using System;
using System.Globalization;
using Foundation;
using NewsApp.iOS.Helpers;
using NewsApp.Interfaces;

[assembly:Xamarin.Forms.Dependency(typeof(Localize))]
namespace NewsApp.iOS.Helpers
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var prefLanguage = "en-US";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = pref.Replace("_", "-");
            }

            CultureInfo cultureInfo = null;
            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch (Exception ex)
            {
                cultureInfo = new CultureInfo(prefLanguage);
            }

            return cultureInfo;
        }
    }
}