using System;
using System.Threading.Tasks;
using Kahua.WeatherApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kahua.WeatherApp;

/// <summary>
///     An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow(MainWindowVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        RootGrid.DataContext = viewModel;
    }

    public MainWindowVM ViewModel { get; }

    public async Task<ContentDialogResult> ShowDialogAsync(string title, string message)
    {
        var warningDialog = new ContentDialog
        {
            Title = title,
            Content = message,
            CloseButtonText = "Ok",
            XamlRoot = RootGrid.XamlRoot
        };
        return await warningDialog.ShowAsync();
    }
}