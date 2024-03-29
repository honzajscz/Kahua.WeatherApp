﻿using System;
using Microsoft.UI.Xaml.Data;

namespace Kahua.WeatherApp.Converters;

public class BoolNegationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool booleanValue) return !booleanValue;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is bool booleanValue) return !booleanValue;
        return value;
    }
}