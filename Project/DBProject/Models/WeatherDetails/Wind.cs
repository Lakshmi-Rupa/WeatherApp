using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Wind", Schema = "dbo")]
    public class Wind
    {
        [Key, Column("wind_id")]
        public int Wind_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("speed")]
        public float Speed { get; set; }
        [Column("deg")]
        public int Deg { get; set; }
    }
}
