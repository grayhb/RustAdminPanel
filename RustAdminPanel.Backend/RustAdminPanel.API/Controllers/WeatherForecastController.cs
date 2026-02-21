using Microsoft.AspNetCore.Mvc;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using System.Diagnostics;

namespace RustAdminPanel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEntityRepository<PlayerConnectionLog> _testRepo;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEntityRepository<PlayerConnectionLog> testRepo)
        {
            _logger = logger;
            _testRepo = testRepo;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<PlayerConnectionLog>> Get()
        {
            var temp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            Debug.WriteLine(temp);

            await _testRepo.AddAsync(new PlayerConnectionLog() { 
                SteamId="test-id-" + DateTime.Now.Millisecond.ToString(),
                SteamName="test name",
                ConnectionIp="127.0.0.1",
                ConnectionTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()
            });

            var items = await _testRepo.GetAllAsync();

            return items;

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
