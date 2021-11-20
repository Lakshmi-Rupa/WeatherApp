using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class LongWeatherForecastModel
    {
        [NotMapped]
        public IEnumerable<LongWeatherForecastListItemModel> List { get; set; }
        [NotMapped]
        public City City { get; set; }
    }
}
