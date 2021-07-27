using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using dotnet_api_whiteapp.Models;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet_api_whiteapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets all the Weather Forecast details.
        /// </summary>
        /// <returns>All Weather forecast details</returns>
        /// <response code="400">If the item is null</response> 
        [HttpGet("GetAllWeatherForecasts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })  
            .ToArray();

            return Ok(forecast);
        }

        /// <summary>
        ///     Gets a Weather Forecast item.
        /// </summary>
        /// <param name="date">weather forecast for a date (YYYY-MM-DD)</param>   
        /// <returns>All Weather forecast details</returns>
        /// <response code="200">Weather forecast item</response> 
        /// <response code="400">If the item is null</response> 
        [HttpGet("GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<WeatherForecast> Get([FromQuery, SwaggerParameter("Weather forecast date", Required = true)] DateTime date)
        {
            var rng = new Random();
            var forecast = new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            return Ok(forecast);
        }

        /// <summary>
        ///     Saves the weather forecast details.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/WeatherForecast/CreateWeatherForecast
        ///     {
        ///        "Date": "2021-07-28T18:48:41.0294769+05:30",
        ///        "TemperatureC": 16,
        ///        "Summary": "Freezing"
        ///     }
        ///     
        /// </remarks>
        /// <param name="weatherForecast">weather forecast details to save</param>   
        /// <returns>A newly Weather forecast item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("CreateWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast weatherForecast)
        {
            return Ok(new WeatherForecast());
        }
    }
}
