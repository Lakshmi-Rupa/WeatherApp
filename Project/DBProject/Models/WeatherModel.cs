using DBProject.Models.WeatherDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class WeatherModel
    {
        public WeatherModel() { }
        [NotMapped]
        public Coord Coord { get; set; }
        [NotMapped]
        public Weather[] Weather { get; set; }
        [NotMapped]
        public Main Main { get; set; }
        [NotMapped]
        public Wind Wind { get; set; }
        [NotMapped]
        public Clouds Clouds { get; set; }
        [NotMapped]
        public Sys Sys { get; set; }
        [NotMapped]
        public int Visibility { get; set; }

        public double Dt { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
    }
}
