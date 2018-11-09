using System.Globalization;
using NewsApp.Droid.Helpers;
using NewsApp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Localize))]
namespace NewsApp.Droid.Helpers
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            CultureInfo result;
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_","-");
            try
            {
                result = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException ex)
            {
                if (netLanguage == "ru-BY")
                {
                    result = new CultureInfo("ru-RU");
                }
                else
                {
                    result = new CultureInfo("en-US");
                }
            }

            return result;
        }
    }
}