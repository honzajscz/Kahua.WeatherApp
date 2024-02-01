using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace Kahua.WeatherApp.Converters
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Set a breakpoint here to inspect the 'value' variable
            System.Diagnostics.Debugger.Break();
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Set a breakpoint here to inspect the 'value' variable
            System.Diagnostics.Debugger.Break();
            return value;
        }
    }
}
