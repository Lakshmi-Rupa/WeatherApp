using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Cloud", Schema = "dbo")]
    public class Clouds
    {
        [Key, Column("cloud_id")]
        public int Cloud_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("cloud_all")]
        public int All { get; set; }
    }
}
