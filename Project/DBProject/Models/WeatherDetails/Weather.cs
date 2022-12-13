using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Weather", Schema = "dbo")]
    public class Weather
    {
        [Key, Column("weather_id")]
        public int Weather_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("main")]
        public string Main { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}
