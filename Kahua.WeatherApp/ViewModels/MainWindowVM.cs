using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kahua.WeatherApp.Services;
using Microsoft.UI.Xaml;

namespace Kahua.WeatherApp.ViewModels;

public class MainWindowVM : ObservableObject
{
    private readonly IGeolocationService _geolocationService;
    private readonly IWeatherService _weatherService;
    private string _dewPoint;
    private bool _inProgress;
    private bool _isCelsius;
    private bool _isLight;
    private double _latitude;
    private double _longitude;
    private string _place;
    private string _temperature;
    private ElementTheme _theme;
    private string _visibility;
    private string _windSpeed;

    public MainWindowVM(IWeatherService weatherService, IGeolocationService geolocationService)
    {
        _weatherService = weatherService;
        _geolocationService = geolocationService;

        GetCurrentLocationCommand = new AsyncRelayCommand(GetCurrentLocationAsync, () => !InProgress);
        SearchWeatherCommand = new AsyncRelayCommand(SearchWeatherAsync, () => !InProgress);
        IsCelsius = true;
        IsLight = true;
        Theme = ElementTheme.Light;
        PropertyChanged += async (sender, args) =>
        {
            if (args.PropertyName == nameof(IsCelsius)) await SearchWeatherAsync();
            if (args.PropertyName == nameof(IsLight)) Theme = IsLight ? ElementTheme.Light : ElementTheme.Dark;
        };
    }

    public bool InProgress
    {
        get => _inProgress;
        set
        {
            if (value == _inProgress) return;
            _inProgress = value;
            OnPropertyChanged();
        }
    }

    public double Latitude
    {
        get => _latitude;
        set
        {
            if (value.Equals(_latitude)) return;
            _latitude = value;
            OnPropertyChanged();
        }
    }

    public double Longitude
    {
        get => _longitude;
        set
        {
            if (value.Equals(_longitude)) return;
            _longitude = value;
            OnPropertyChanged();
        }
    }

    public string Visibility
    {
        get => _visibility;
        set
        {
            if (value.Equals(_visibility)) return;
            _visibility = value;
            OnPropertyChanged();
        }
    }

    public string WindSpeed
    {
        get => _windSpeed;
        set
        {
            if (value == _windSpeed) return;
            _windSpeed = value;
            OnPropertyChanged();
        }
    }

    public string DewPoint
    {
        get => _dewPoint;
        set
        {
            if (value == _dewPoint) return;
            _dewPoint = value;
            OnPropertyChanged();
        }
    }

    public string Temperature
    {
        get => _temperature;
        set
        {
            if (value == _temperature) return;
            _temperature = value;
            OnPropertyChanged();
        }
    }

    public string Place
    {
        get => _place;
        set
        {
            if (value == _place) return;
            _place = value;
            OnPropertyChanged();
        }
    }

    public bool IsCelsius
    {
        get => _isCelsius;
        set
        {
            if (value == _isCelsius) return;
            _isCelsius = value;
            IsCelsius = value;
            OnPropertyChanged();
        }
    }

    public ElementTheme Theme
    {
        get => _theme;
        set
        {
            if (value == _theme) return;
            _theme = value;
            OnPropertyChanged();
        }
    }

    public bool IsLight
    {
        get => _isLight;
        set
        {
            if (value == _isLight) return;
            _isLight = value;
            OnPropertyChanged();
        }
    }

    public ICommand SearchWeatherCommand { get; init; }

    public ICommand GetCurrentLocationCommand { get; set; }

    private async Task SearchWeatherAsync()
    {
        InProgress = true;
        try
        {
            var weatherDataAsync = await _weatherService.GetWeatherDataAsync(Latitude, Longitude,
                IsCelsius ? WeatherUnits.Celsius : WeatherUnits.Fahrenheit);
            var tempUnit = IsCelsius ? "°C" : "°F";
            Temperature = $"{weatherDataAsync.Temperature:F1} {tempUnit}";
            WindSpeed = $"{weatherDataAsync.WindSpeed:F1} kn";
            DewPoint = $"{weatherDataAsync.DewPoint:F1} {tempUnit}";
            Visibility = $"{weatherDataAsync.Visibility:F2} km";
            Place = $"{weatherDataAsync.Location.Place}, {weatherDataAsync.Location.Country}";
        }
        finally
        {
            InProgress = false;
        }
    }

    public async Task GetCurrentLocationAsync()
    {
        InProgress = true;
        try
        {
            var location = await _geolocationService.GetCurrentLocationAsync();
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }
        finally
        {
            InProgress = false;
        }
    }
}