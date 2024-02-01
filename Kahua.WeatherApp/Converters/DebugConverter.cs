using System;
using System.Diagnostics;
using Microsoft.UI.Xaml.Data;

namespace Kahua.WeatherApp.Converters;

public class DebugConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Set a breakpoint here to inspect the 'value' variable
        Debugger.Break();
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        // Set a breakpoint here to inspect the 'value' variable
        Debugger.Break();
        return value;
    }
}