using DBProject.Models.WeatherDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class WeatherModel
    {
        public WeatherModel() { }

        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public Sys Sys { get; set; }
        public int Visibility { get; set; }

        public double Dt { get; set; }

        // City id
        public int Id { get; set; }

        //City name
        public string Name { get; set; }
    }
}
