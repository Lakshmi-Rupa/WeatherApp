using DBProject.Models;
using DBProject.Models.WeatherDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherByCityNameAsync(string cityName);
        Task<WeatherModel> GetWeatherByCityIDAsync(int cityID);
        Task<WeatherModel> GetWeatherByCityCoordinatesAsync(Coord coordinates);
        Task<LongWeatherForecastModel> GetLongWeatherForecastAsync(string cityName);
    }
}
