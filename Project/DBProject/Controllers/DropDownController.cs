using DBProject.Models;
using DBProject.Models.WeatherDetails;
using DBProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DropDownController : ControllerBase
    {
        private readonly ILogger<DropDownController> _logger;
        private readonly WeatherDbContext _context;

        public DropDownController(ILogger<DropDownController> logger
            , WeatherDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("cityDropdown")]
        public async Task<ActionResult<IEnumerable<City>>> GetCityDropdown()
        {
            return await _context.City.OrderBy(x => x.CityId).ToListAsync();
        }

        [HttpGet("cloudsDropdown")]
        public async Task<ActionResult<IEnumerable<Clouds>>> GetCloudsDropdown()
        {
            return await _context.Clouds.OrderBy(x => x.Cloud_id).ToListAsync();
        }

        [HttpGet("coordDropdown")]
        public async Task<ActionResult<IEnumerable<Coord>>> GetCoordDropdown()
        {
            return await _context.Coord.OrderBy(x => x.CoordId).ToListAsync();
        }

        [HttpGet("mainDropdown")]
        public async Task<ActionResult<IEnumerable<Main>>> GetMainDropdown()
        {
            return await _context.Main.OrderBy(x => x.Main_id).ToListAsync();
        }

        [HttpGet("sysDropdown")]
        public async Task<ActionResult<IEnumerable<Sys>>> GetSysDropdown()
        {
            return await _context.Sys.OrderBy(x => x.Sys_id).ToListAsync();
        }

        [HttpGet("weatherDropdown")]
        public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherDropdown()
        {
            return await _context.Weather.OrderBy(x => x.Weather_id).ToListAsync();
        }

        [HttpGet("windDropdown")]
        public async Task<ActionResult<IEnumerable<Wind>>> GetWindDropdown()
        {
            return await _context.Wind.OrderBy(x => x.Wind_id).ToListAsync();
        }

        [HttpGet("getWeatherGrid")]
        public async Task<ActionResult<IEnumerable<WeatherGrid>>> GetWeatherGrid()
        {
            return await _context.WeatherGrid.OrderBy(x => x.CityId).ToListAsync();
        }
    }
}
