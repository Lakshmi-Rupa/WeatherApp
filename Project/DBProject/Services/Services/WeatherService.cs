﻿using DBProject.Data;
using DBProject.Models;
using DBProject.Services.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBProject.Models.WeatherDetails;

namespace DBProject.Services.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly IClientService _clientService;
        private readonly ApplicationData _applicationData;


        public WeatherService(IClientService clientService, IOptions<ApplicationData> applicationData)
        {
            _clientService = clientService;
            _applicationData = applicationData.Value;
        }

        public async Task<LongWeatherForecastModel> GetLongWeatherForecastAsync(string cityName)
        {
            try
            {
                var requestUrl = _applicationData.OWMForecastUrl + $"q={cityName}&appid={_applicationData.OWMApiKey}";
                var response = await _clientService.GetAsync<LongWeatherForecastModel>(requestUrl);
                // http://api.openweathermap.org/data/2.5/forecast?q=Tczew&appid=f1b58a47aa129daa6330e7280a88e7b5
                return response;
            }
            catch (InvalidOperationException ex)
            {

                Log.Debug(ex, "A invalid content type");
                return null;
            }
        }

        public async Task<WeatherModel> GetWeatherByCityCoordinatesAsync(Coord coordinates)
        {

            if (coordinates == null)
            {
                Log.Error("City coordinates are null");

                throw new ArgumentNullException(nameof(coordinates));
            }

            try
            {
                var requestURl = _applicationData.OWMUrl + $"lat={coordinates.Lat}&lon={coordinates.Lon}&" + "appid=" + _applicationData.OWMApiKey;
                var response = await _clientService.GetAsync<WeatherModel>(requestURl);
                return response;

            }
            catch (InvalidOperationException ex)
            {
                Log.Debug(ex, "A invalid content type");
                return null;
            }


        }

        public async Task<WeatherModel> GetWeatherByCityIDAsync(int cityID)
        {
            try
            {
                var response = await _clientService.GetAsync<WeatherModel>(_applicationData.OWMUrl + $"id={cityID}&" + "appid=" + _applicationData.OWMApiKey);

                return response;

            }
            catch (InvalidOperationException ex)
            {
                Log.Debug(ex, "A invalid content type");
                return null;
            }

        }

        public async Task<WeatherModel> GetWeatherByCityNameAsync(string cityName)
        {
            try
            {
                var response = await _clientService.GetAsync<WeatherModel>(_applicationData.OWMUrl + $"q={cityName}&" + "appid=" + _applicationData.OWMApiKey);

                return response;

            }
            catch (InvalidOperationException ex)
            {
                Log.Debug(ex, "A invalid content type");
                return null;
            }
        }
    }
}
