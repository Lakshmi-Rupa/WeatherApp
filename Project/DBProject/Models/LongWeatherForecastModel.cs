using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class LongWeatherForecastModel
    {
        public IEnumerable<LongWeatherForecastListItemModel> List { get; set; }
        public City City { get; set; }
    }
}
