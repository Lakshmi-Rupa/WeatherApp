using DBProject.Helpers;
using DBProject.Models;
using DBProject.Models.WeatherDetails;
using DBProject.Services;
using DBProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherDbContext _context;

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger
            , WeatherDbContext context)
        {
            _weatherService = weatherService;
            _context = context;
            _logger = logger;
        }
        //api/weather/currentweather
        [HttpGet("currentWeatherByCity/{city}")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityName(string city)
        {

            //Log.Information($"Getting Current Weather by city id from {Request.Headers["Origin"]}");
            var response = await _weatherService.GetWeatherByCityNameAsync(city);

            var cityExists = _context.City.Where(x => x.Id == response.Id).ToList();
            var city_id = 0;
            if (cityExists.Count == 0)
            {
                #region 
                City cityModel = new City();
                cityModel.Name = response.Name;
                cityModel.Id = response.Id;
                cityModel.CityId = 0;
                cityModel.Timezone = response.Timezone;
                cityModel.Country = response.Sys.Country;
                _context.City.Add(cityModel);
                await _context.SaveChangesAsync();
                city_id = cityModel.CityId;
                #endregion

                #region
                Clouds cloudModel = new Clouds();
                cloudModel.Cloud_id = 0;
                cloudModel.CityId = city_id;
                cloudModel.All = response.Clouds.All;
                _context.Clouds.Add(cloudModel);
                await _context.SaveChangesAsync();
                #endregion

                #region
                Sys sysModel = new Sys();
                sysModel.Sys_id = 0;
                sysModel.CityId = city_id;
                sysModel.Sunrise = response.Sys.Sunrise;
                sysModel.Sunset = response.Sys.Sunset;
                sysModel.Country = response.Sys.Country;
                _context.Sys.Add(sysModel);
                await _context.SaveChangesAsync();
                #endregion

                #region
                Main mainModel = new Main();
                mainModel.Main_id = 0;
                mainModel.CityId = city_id;
                mainModel.Pressure = response.Main.Pressure;
                mainModel.Temp = response.Main.Temp;
                mainModel.Temp_max = response.Main.Temp_max;
                mainModel.Temp_min = response.Main.Temp_min;
                mainModel.Humidity = response.Main.Humidity;
                _context.Main.Add(mainModel);
                await _context.SaveChangesAsync();
                #endregion

                #region
                Coord coordModel = new Coord();
                coordModel.Coord_id = 0;
                coordModel.CityId = city_id;
                coordModel.Lat = response.Coord.Lat;
                coordModel.Lon = response.Coord.Lon;
                _context.Coord.Add(coordModel);
                await _context.SaveChangesAsync();
                #endregion

                #region
                Weather weatherModel = new Weather();
                weatherModel.Weather_id = 0;
                weatherModel.CityId = city_id;
                weatherModel.Description = response.Weather.Select(x => x.Description).Single().ToString();
                weatherModel.Main = response.Weather.Select(x => x.Main ).Single().ToString();
                _context.Weather.Add(weatherModel);
                await _context.SaveChangesAsync();
                #endregion

                #region
                Wind windModel = new Wind();
                windModel.Wind_id = 0;
                windModel.CityId = city_id;
                windModel.Deg = response.Wind.Deg;
                windModel.Speed = response.Wind.Speed;
                _context.Wind.Add(windModel);
                await _context.SaveChangesAsync();
                #endregion
            }



            if (response == null)
            {
                var res = new ApiResponse<string>
                {
                    ResponseBody = $"Cannot find weather for ${ city}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(res);
            }

            var result = new ApiResponse<WeatherModel>
            {
                ResponseBody = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return Ok(result);
        }


        //api/weather/currentweather
        [HttpGet("currentWeatherByCityID/{id}")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityID(int id)
        {

            //Log.Information($"Getting Current Weather by city id from {Request.Headers["Origin"]}");

            var response = await _weatherService.GetWeatherByCityIDAsync(id);

            if (response == null)
            {
                var res = new ApiResponse<string>
                {
                    ResponseBody = $"Cannot find city with id of {id}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(res);
            }

            var result = new ApiResponse<WeatherModel>
            {
                ResponseBody = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return Ok(result);

        }

        [HttpGet("currentWeatherByCityCoord")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityCoord([FromQuery] Coord coord)
        {
            //Log.Information($"Getting Current Weather by city id from {Request.Headers["Origin"]}");

            var response = await _weatherService.GetWeatherByCityCoordinatesAsync(coord);

            if (response == null)
            {
                var res = new ApiResponse<string>
                {
                    ResponseBody = $"Cannot find city with coords of {coord}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(res);
            }

            var result = new ApiResponse<WeatherModel>
            {
                ResponseBody = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return Ok(result);
        }

        [HttpGet("longWeatherForecast/{cityName}")]
        public async Task<ActionResult<ApiResponse<LongWeatherForecastModel>>> GetLongWeatherForecast(string cityName)
        {
            var response = await _weatherService.GetLongWeatherForecastAsync(cityName);

            if (response == null)
            {
                var res = new ApiResponse<string>
                {
                    ResponseBody = $"Cannot find weather for ${ cityName}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(res);
            }


            var resultModel = new LongWeatherForecastModel
            {
                List = WeatherForecastHelper.GetFilteredItems(response.List),
                City = response.City
            };


            var result = new ApiResponse<LongWeatherForecastModel>
            {
                ResponseBody = resultModel,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return Ok(result);
        }

    }
}
