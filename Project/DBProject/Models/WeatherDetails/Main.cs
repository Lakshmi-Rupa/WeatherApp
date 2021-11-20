using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    [Table("Main", Schema = "dbo")]
    public class Main
    {
        [Key, Column("main_id")]
        public int Main_id { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("temp")]
        public float Temp { get; set; }
        [Column("pressure")]
        public int Pressure { get; set; }
        [Column("humidity")]
        public int Humidity { get; set; }
        [Column("temp_min")]
        public float Temp_min { get; set; }
        [Column("temp_max")]
        public float Temp_max { get; set; }
        public int TempC { get => (int)Math.Round(Temp - 273.15); }
    }
}
