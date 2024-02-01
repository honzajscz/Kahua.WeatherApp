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
using Kahua.WeatherApp.ViewModels;
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
        public MainWindowVM ViewModel { get; }

        public MainWindow(MainWindowVM viewModel)
        {
            
            ViewModel = viewModel;
            this.InitializeComponent();
            RootGrid.DataContext = viewModel;
        }

        public async Task<ContentDialogResult> ShowDialogAsync(string title, string message)
        {
            ContentDialog warningDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok",
                XamlRoot = RootGrid.XamlRoot
            };
            return await warningDialog.ShowAsync();
        }
    }
}
