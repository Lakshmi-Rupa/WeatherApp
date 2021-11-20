using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Coord", Schema = "dbo")]
    public class Coord
    {
        [Key, Column("coord_id")]
        public int Coord_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("Lon")]
        public float Lon { get; set; }
        [Column("Lat")]
        public float Lat { get; set; }

        public override string ToString()
        {
            return $"lat: {Lat}, lon: {Lon}";
        }
    }
}
