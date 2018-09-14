using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ticks = DateTime.Now.TimeOfDay.Ticks - ((DateTime) value).TimeOfDay.Ticks;
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
                ticks = DateTime.Now.Ticks - ((DateTime) value).Ticks;
                var timeAgo = new DateTime(ticks);
                return timeAgo.Day + "d ago";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}
