﻿using DBProject.Helpers;
using DBProject.Models;
using DBProject.Models.WeatherDetails;
using DBProject.Services;
using DBProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Controllers
{
    [ApiController, Route("api/[controller]")]
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

            var cityExists = _context.City.Where(x => x.Id == response.Id && x.DeleteIndicator == false).ToList();

            if (cityExists.Count == 0)
            {
                await insertWeather(response);
            }
            else
            {
                await updateWeather(response, cityExists);
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

        public async Task<ActionResult> insertWeather(WeatherModel response)
        {
            var city_id = 0;
            #region 
            City cityModel = new City();
            cityModel.Name = response.Name;
            cityModel.Id = response.Id;
            cityModel.CityId = 0;
            cityModel.Timezone = response.Timezone;
            cityModel.Country = response.Sys.Country;
            cityModel.CreatedDate = DateTime.Now.Date;
            cityModel.UpdatedDate = DateTime.Now.Date;
            cityModel.DeleteIndicator = false;
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
            coordModel.CoordId = 0;
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
            weatherModel.Main = response.Weather.Select(x => x.Main).Single().ToString();
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

            return Ok();
        }
        public async Task<ActionResult> updateWeather([FromRoute]WeatherModel response, List<City> cityExists)
        {
            var city_id = 0;
            #region 
            var cityModel = default(City);
            cityModel = await _context.City.FindAsync(cityExists.Select(x => x.CityId).Single());
            cityModel.Name = response.Name;
            cityModel.Id = response.Id;
            cityModel.Timezone = response.Timezone;
            cityModel.Country = response.Sys.Country;
            cityModel.UpdatedDate = DateTime.Now.Date;
            _context.Entry(cityModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            city_id = cityModel.CityId;
            #endregion

            #region
            var cloudExists = _context.Clouds.Where(x => x.CityId == city_id).ToList();
            var cloudModel = default(Clouds);
            cloudModel = await _context.Clouds.FindAsync(cloudExists.Select(x => x.Cloud_id).Single());
            cloudModel.All = response.Clouds.All;
            _context.Entry(cloudModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            #region
            var sysExists = _context.Sys.Where(x => x.CityId == city_id).ToList();
            var sysModel = default(Sys);
            sysModel = await _context.Sys.FindAsync(sysExists.Select(x => x.Sys_id).Single());
            sysModel.Sunrise = response.Sys.Sunrise;
            sysModel.Sunset = response.Sys.Sunset;
            sysModel.Country = response.Sys.Country;
            _context.Entry(sysModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            #region
            var mainExists = _context.Main.Where(x => x.CityId == city_id).ToList();
            var mainModel = default(Main);
            mainModel = await _context.Main.FindAsync(mainExists.Select(x => x.Main_id).Single());
            mainModel.Pressure = response.Main.Pressure;
            mainModel.Temp = response.Main.Temp;
            mainModel.Temp_max = response.Main.Temp_max;
            mainModel.Temp_min = response.Main.Temp_min;
            mainModel.Humidity = response.Main.Humidity;
            _context.Entry(mainModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            #region
            var coordExists = _context.Coord.Where(x => x.CityId == city_id).ToList();
            var coordModel = default(Coord);
            coordModel = await _context.Coord.FindAsync(coordExists.Select(x => x.CoordId).Single());
            coordModel.Lat = response.Coord.Lat;
            coordModel.Lon = response.Coord.Lon;
            _context.Entry(coordModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            #region
            var weatherExists = _context.Weather.Where(x => x.CityId == city_id).ToList();
            var weatherModel = default(Weather);
            weatherModel = await _context.Weather.FindAsync(weatherExists.Select(x => x.Weather_id).Single());
            weatherModel.Description = response.Weather.Select(x => x.Description).Single().ToString();
            weatherModel.Main = response.Weather.Select(x => x.Main).Single().ToString();
            _context.Entry(weatherModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            #region
            var windExists = _context.Wind.Where(x => x.CityId == city_id).ToList();
            var windModel = default(Wind);
            windModel = await _context.Wind.FindAsync(windExists.Select(x => x.Wind_id).Single());
            windModel.Deg = response.Wind.Deg;
            windModel.Speed = response.Wind.Speed;
            _context.Entry(windModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            #endregion

            return Ok();
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

            var cityExists = _context.City.Where(x => x.Id == response.City.Id && x.DeleteIndicator == false).ToList();
            var cityModel = default(City);
            cityModel = await _context.City.FindAsync(cityExists.Select(x => x.CityId).Single());
            
            cityModel.Population = response.City.Population;
            cityModel.UpdatedDate = DateTime.Now.Date;
            _context.Entry(cityModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
