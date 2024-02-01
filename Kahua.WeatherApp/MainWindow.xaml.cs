using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using SolidColorBrush = ABI.Microsoft.UI.Xaml.Media.SolidColorBrush;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kahua.WeatherApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
        }

        public async Task<ContentDialogResult> ShowDialogAsync(string title, string message)
        {
            ContentDialog warningDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok",
                XamlRoot = Grid.XamlRoot
            };
            return await warningDialog.ShowAsync();
        }

        
        public async Task<Geoposition> GetCurrentLocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    var geolocator = new Geolocator { DesiredAccuracyInMeters = 50 };
                    var position = await geolocator.GetGeopositionAsync();
                    return position;
                case GeolocationAccessStatus.Denied:
                    throw new Exception("Location access denied.");
                case GeolocationAccessStatus.Unspecified:
                    throw new Exception("Unspecified error.");
                default:
                    throw new Exception("Unknown error.");
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var currentLocationAsync = await GetCurrentLocationAsync();
            
        }
    }
}
