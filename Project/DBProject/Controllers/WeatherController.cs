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

            City cityModel = new City();
            cityModel.Name = response.Name;
            cityModel.Id = response.Id;
            cityModel.cityId = 0;

            _context.City.Add(cityModel);
            await _context.SaveChangesAsync();

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
