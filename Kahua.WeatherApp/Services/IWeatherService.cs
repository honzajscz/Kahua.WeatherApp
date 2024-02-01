using System.Threading.Tasks;

namespace Kahua.WeatherApp.Services;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude, WeatherUnits units);
}