using System;

namespace RustAdminPanel.API
{
    public class WeatherForecast
    {
        public WeatherForecast()
        {
            
        }

        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
