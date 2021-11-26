using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    [Table("WeatherGrid", Schema = "dbo")]
    public class WeatherGrid
    {
        [Key, Column("city_id")]
        public int CityId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("cloud_all")]
        public int Cloudiness { get; set; }
        [Column("temp")]
        public double Temperature { get; set; }
        [Column("main")]
        public string WeatherCondition { get; set; }
        [Column("speed")]
        public double WindSpeed { get; set; }
        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updatedDate")]
        public DateTime? UpdatedDate { get; set; }

    }
}
