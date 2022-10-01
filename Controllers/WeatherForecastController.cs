using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.Controllers
***REMOVED***
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    ***REMOVED***
        private static readonly string[] Summaries = new[]
        ***REMOVED***
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
***REMOVED***;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        ***REMOVED***
            _logger = logger;
***REMOVED***

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        ***REMOVED***
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            ***REMOVED***
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
    ***REMOVED***)
            .ToArray();
***REMOVED***
***REMOVED***
***REMOVED***
