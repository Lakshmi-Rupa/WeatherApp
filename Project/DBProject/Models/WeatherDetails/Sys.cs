using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Sys", Schema = "dbo")]
    public class Sys
    {
        [Key, Column("sys_id")]
        public int Sys_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("sunrise")]
        public double Sunrise { get; set; }
        [Column("sunset")]
        public double Sunset { get; set; }

        public string SunsetTime
        {
            get
            {
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(Sunset * 1000);
                return date.ToShortTimeString();
            }
        }

        public string SunriseTime
        {
            get
            {
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(Sunrise * 1000);
                return date.ToShortTimeString();
            }
        }
    }
}
