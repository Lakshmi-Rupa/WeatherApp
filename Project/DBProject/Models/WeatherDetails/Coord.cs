using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models.WeatherDetails
{
    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }

        public override string ToString()
        {
            return $"lat: {Lat}, lon: {Lon}";
        }
    }
}
