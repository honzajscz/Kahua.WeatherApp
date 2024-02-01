using System.Threading.Tasks;
using OpenWeather;

namespace Kahua.WeatherApp.Services;

public interface IGeolocationService
{
    Task<Location> GetCurrentLocationAsync();
}