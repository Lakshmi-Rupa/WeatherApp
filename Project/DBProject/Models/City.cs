using DBProject.Models.WeatherDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBProject.Models
{
    [Table("City", Schema = "dbo")]
    public class City
    {
        [Key, Column("city_id")]
        public int CityId { get; set; }
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        //public Coord Coord { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("population")]
        public string Population { get; set; }
        [Column("timezone")]
        public string Timezone { get; set; }
    }
}
