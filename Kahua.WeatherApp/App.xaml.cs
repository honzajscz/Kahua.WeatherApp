using System.Globalization;
using Windows.Graphics;
using Kahua.WeatherApp.Services;
using Kahua.WeatherApp.ViewModels;
using Microsoft.UI.Xaml;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kahua.WeatherApp;

/// <summary>
///     Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private MainWindow _window;

    /// <summary>
    ///     Initializes the singleton application object.  This is the first line of authored code
    ///     executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        InitializeComponent();
        UnhandledException += OnUnhandledException;
    }

    private async void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        await _window.ShowDialogAsync("Error occured :-(", e.Message);
    }


    /// <summary>
    ///     Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        // poor man's DI
        var weatherService = new WeatherService();
        var myGeolocator = new GeolocationService();
        var viewModel = new MainWindowVM(weatherService, myGeolocator);
        _window = new MainWindow(viewModel);
        _window.Activate();
        _window.AppWindow.Resize(new SizeInt32(650, 350));
    }
}