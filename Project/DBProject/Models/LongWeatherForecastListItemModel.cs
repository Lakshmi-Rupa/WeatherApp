using DBProject.Helpers;
using DBProject.Models.WeatherDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBProject.Models
{
    public class LongWeatherForecastListItemModel
    {
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Weather[] Weather { get; set; }
        public Clouds Clouds { get; set; }

        public string Dt_txt { get; set; }
        public string DayName { get => WeatherForecastHelper.GetDayNameFromDate(WeatherForecastDateTime); }
        public DateTime WeatherForecastDateTime { get => DateTime.Parse(Dt_txt); }
    }
}
