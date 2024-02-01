using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using OpenWeather;

namespace Kahua.WeatherApp.Services;

class GeolocationService : IGeolocationService
{
    public async Task<Location> GetCurrentLocationAsync()
    {
        var accessStatus = await Geolocator.RequestAccessAsync();
        switch (accessStatus)
        {
            case GeolocationAccessStatus.Allowed:
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 50 };
                var position = await geolocator.GetGeopositionAsync();
                return new (position.Coordinate.Latitude, position.Coordinate.Longitude) ;
            case GeolocationAccessStatus.Denied:
                throw new ApplicationException("Location access denied.");
            case GeolocationAccessStatus.Unspecified:
                throw new ApplicationException("Unspecified error.");
            default:
                throw new ApplicationException("Unknown error.");
        }
    }
}