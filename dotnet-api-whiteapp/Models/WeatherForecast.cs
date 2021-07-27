using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api_swagger.Models
{
    public class WeatherForecast
    {
        /// <summary>
        /// The Date of the Weather Forecast
        /// </summary>
        /// <example>2021-07-27</example>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The Temperature (in celsius) of the Weather Forecast
        /// </summary>
        /// <example>2021-07-27</example>
        public int TemperatureC { get; set; }

        [Obsolete]
        [SwaggerSchema("The Temperature in Fahrenheit (calculated)", ReadOnly = true)]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// The summary of the Weather Forecast
        /// </summary>
        /// <example>warm</example>
        [DefaultValue("normal")]
        public string Summary { get; set; }
    }
}
