using System;
using Microsoft.UI.Xaml.Data;

namespace Kahua.WeatherApp.Converters;

public class BoolToOpacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool booleanValue) return booleanValue ? 1 : 0;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}