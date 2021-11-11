﻿using DBProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Helpers
{
    public class WeatherForecastHelper
    {
        public static IEnumerable<LongWeatherForecastListItemModel> GetFilteredItems(IEnumerable<LongWeatherForecastListItemModel> list)
        {
            List<LongWeatherForecastListItemModel> items = new List<LongWeatherForecastListItemModel>();

            int index = 0;

            foreach (var item in list)
            {
                if (index == 0)
                    items.Add(item);

                index += 1;

                if (index % 8 == 0)
                    items.Add(item);
            }

            return items;
        }

        public static string GetDayNameFromDate(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }
    }
}
