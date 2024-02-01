namespace Kahua.WeatherApp.Services;

public record WeatherData(
    double Temperature,
    double WindSpeed,
    double DewPoint,
    double Visibility,
    WeatherStationLocation Location);