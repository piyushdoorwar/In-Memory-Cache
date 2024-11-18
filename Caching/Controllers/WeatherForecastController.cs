using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IMemoryCache cache) : ControllerBase
    {
        private readonly IMemoryCache _cache = cache;
        private readonly HttpClient _httpClient = new();

        [HttpGet]
        public async Task<string> Get([FromQuery] string cityCode)
        {
            if (_cache.TryGetValue(cityCode, out string weatherData))
            {
                return weatherData; // Return cached data
            }
            // Fetch data from API
            var response = await _httpClient.GetStringAsync($"https://api.weatherapi.com/v1/current.json?q={cityCode}&key=f7d132db2ce54be49ee81138241811");

            // Cache the data
            _cache.Set(cityCode, response, TimeSpan.FromMinutes(2));
            return response;
        }
    }
}
