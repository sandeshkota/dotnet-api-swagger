using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api_whiteapp.Models
{
    public class WeatherForecast
    {
        [Required]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [DefaultValue("normal")]
        public string Summary { get; set; }
    }
}
