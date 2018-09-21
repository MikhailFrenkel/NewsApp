using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace NewsApp.Converters
{
    /// <summary>
    /// Shows how much time has passed.
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts DateTime to TimeAgo.
        /// </summary>
        /// <param name="value">DateTime.</param>
        /// <param name="targetType">Expected type.</param>
        /// <param name="parameter">Addition parameter.</param>
        /// <param name="culture">Information about culture.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var ticks = DateTime.Now.TimeOfDay.Ticks - ((DateTime)value).TimeOfDay.Ticks;
                if (ticks > 0)
                {
                    var timeAgo = new DateTime(ticks);
                    if (timeAgo.TimeOfDay.Hours == 0)
                        return timeAgo.Minute + "min ago";
                    else
                        return timeAgo.Hour + "h ago";
                }
                else
                {
                    ticks = DateTime.Now.Ticks - ((DateTime)value).Ticks;
                    var timeAgo = new DateTime(ticks);
                    return timeAgo.Day + "d ago";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"DateTimeConverterException : {e.Message}");
                return "";
            }
        }

        /// <summary>
        /// Converts from timeAgo to DateTime
        /// </summary>
        /// <param name="value">TimeAgo.</param>
        /// <param name="targetType">DateTime.</param>
        /// <param name="parameter">Addition parameter.</param>
        /// <param name="culture">Information about  culture.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}
