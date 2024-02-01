using System;
using System.Threading.Tasks;
using OpenWeather;
using UnitsNet.Units;

namespace Kahua.WeatherApp.Services;

internal class WeatherService : IWeatherService
{
    public const int TimeOutSec = 10;

    public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude, WeatherUnits units)
    {
        try
        {
            if (!StationDictionary.TryGetClosestStation(latitude, longitude, out var stationInfo))
                throw new ApplicationException("Weather station not found");

            var weather = await stationInfo.AsMetarStation().Update().WaitAsync(TimeSpan.FromSeconds(TimeOutSec));
            if (weather == null)
                throw new ApplicationException($"No weather data in the station {stationInfo.ICAO}");


            var weatherInUnits = weather.Value.ConvertTo(new Units(units == WeatherUnits.Celsius
                    ? TemperatureUnit.DegreeCelsius
                    : TemperatureUnit.DegreeFahrenheit,
                PressureUnit.Hectopascal));

            var region = string.IsNullOrWhiteSpace(stationInfo.Region) ? string.Empty : $" ({stationInfo.Region})";
            var location = new WeatherStationLocation(stationInfo.Country, stationInfo.Name + region);


            return new WeatherData(weatherInUnits.Temperature, weatherInUnits.WindSpeed, weatherInUnits.Dewpoint,
                weatherInUnits.Visibility, location);
        }
        catch (Exception e)
        {
            throw new ApplicationException($"Failed getting weather data: '{e.Message}'");
        }
    }
}