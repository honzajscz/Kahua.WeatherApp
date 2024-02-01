using CommunityToolkit.Mvvm.ComponentModel;
using OpenWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using UnitsNet.Units;
using ApplicationException = System.ApplicationException;

namespace Kahua.WeatherApp.ViewModels
{
    public enum WeatherUnits
    {
        Celsius,
        Fahrenheit
    }

    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude, WeatherUnits units);
    }

    class WeatherService : IWeatherService
    {
        public const int TimeOutSec = 10;

        public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude, WeatherUnits units)
        {
            try
            {
                if (!StationDictionary.TryGetClosestStation(latitude, longitude, out var stationInfo))
                {
                    throw new ApplicationException("Weather station not found");
                }

                var weather = await stationInfo.AsMetarStation().Update().WaitAsync(TimeSpan.FromSeconds(TimeOutSec));
                if (weather == null)
                    throw new ApplicationException($"No weather data in the station {stationInfo.ICAO}");


                var weatherInUnits = weather.Value.ConvertTo(new Units(units == WeatherUnits.Celsius
                        ? TemperatureUnit.DegreeCelsius
                        : TemperatureUnit.DegreeFahrenheit,
                    PressureUnit.Hectopascal, SpeedUnit.Knot, LengthUnit.Kilometer));

                var region = string.IsNullOrWhiteSpace(stationInfo.Region) ? string.Empty : $"({stationInfo.Region})";
                var location = new WeatherStationLocation(stationInfo.Country, stationInfo.Name + region);


                return new WeatherData(weatherInUnits.Temperature, weatherInUnits.WindSpeed, weatherInUnits.Dewpoint,
                    weatherInUnits.Pressure, location);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Failed getting weather data: '{e.Message}'");
            }
        }
    }

    public record WeatherData(
        double Temperature,
        double WindSpeed,
        double DewPoint,
        double Pressure,
        WeatherStationLocation Location);

    public record WeatherStationLocation(string Country, string Place);

    class MainWindowVM : ObservableObject
    {
        private readonly IWeatherService _weatherService;
        private bool _inProgress;
        private double _latitude;
        private double _longitude;
        private string _pressure;
        private string _windSpeed;
        private string _dewPoint;
        private string _temperature;
        private string _place;
        private bool _isCelsius;

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

        public string Pressure
        {
            get => _pressure;
            set
            {
                if (value.Equals(_pressure)) return;
                _pressure = value;
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
                OnPropertyChanged();
            }
        }

        public ICommand Search { get; init; }


        private async Task CancelableExecute(CancellationToken token)
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
                Pressure = $"{weatherDataAsync.Pressure:F1} hPa";
                Place = weatherDataAsync.Location.Place;
            }
            finally
            {
                InProgress = false;
            }
        }

  
        public MainWindowVM(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            Search = new AsyncRelayCommand(CancelableExecute);
            IsCelsius = true;
        }
    }
}