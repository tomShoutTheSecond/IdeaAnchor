using System;
using System.Globalization;
using IdeaAnchor.Helper;

namespace IdeaAnchor.ValueConverters
{
    public class TimeStampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeHelper.GetReadableTime(TimeHelper.GetDateTime((string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

