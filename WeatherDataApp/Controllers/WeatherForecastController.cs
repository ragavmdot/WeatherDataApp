using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using System.Text.Json;

namespace WeatherDataApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public WeatherForecastController(IHttpClientFactory httpClientFactory, IConfiguration configuration, AppDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        [Route("AddorUpdateWeatherData")]
        public async Task<IActionResult> AddWeatherForecastData(WeatherDataRequest request)
        {

            // if record already exists
            var existingData = await _context.WeatherData.FirstOrDefaultAsync(x => x.Latitude == request.Latitude && x.Longitude == request.Longitude);

            var baseUrl = _configuration["openMeteoBaseUrl"];
            var sampleApiUrl = $"{baseUrl}?latitude={request.Latitude}&longitude={request.Longitude}&hourly=,temperature_2m&forecast_days=1&temperature_unit=fahrenheit";

            var response = await _httpClient.GetAsync(sampleApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to fetch data from sample API.");
            }

            var content = await response.Content.ReadAsStringAsync();

            WeatherDataResponse weatherData = JsonSerializer.Deserialize<WeatherDataResponse>(content);
            WeatherDataModel weatherDataModel = new WeatherDataModel();
            if (weatherData != null)
            {
                weatherDataModel = new WeatherDataModel
                {
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    WeatherData = JsonSerializer.Serialize(weatherData)
                };

            }

            if (existingData == null)
            {
                await _context.WeatherData.AddAsync(weatherDataModel);
            }
            else
            {
                existingData.WeatherData = weatherDataModel.WeatherData;
            }

            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpPost]
        [Route("GetWeatherData")]
        public async Task<IActionResult> GetWeatherForecastData(WeatherDataRequest request)
        {
            var content = await _context.WeatherData.FirstOrDefaultAsync(x => x.Latitude == request.Latitude && x.Longitude == request.Longitude);

            if (content == null)
            {
                return NotFound("Record doesn't exist.");
            }

            var weatherData = JsonSerializer.Deserialize<WeatherDataResponse>(content.WeatherData);

            return Ok(weatherData);
        }

        [HttpDelete]
        [Route("DeleteWeatherData")]
        public async Task<IActionResult> DeleteWeatherForecastData(WeatherDataRequest request)
        {
            var content = await _context.WeatherData.FirstOrDefaultAsync(x => x.Latitude == request.Latitude && x.Longitude == request.Longitude);

            if (content == null)
            {
                return NotFound("Record doesn't exist to be deleted.");
            }

            _context.WeatherData.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("GetAllWeatherData")]
        public async Task<IActionResult> GetExistingWeatherForecastData()
        {
            var content = await _context.WeatherData.ToListAsync();

            if (content == null)
            {
                return NotFound("No records exist.");
            }

            List<WeatherDataResponse> recordsList = new List<WeatherDataResponse>();
            foreach (var item in content)
            {
                recordsList.Add(JsonSerializer.Deserialize<WeatherDataResponse>(item.WeatherData));
            }

            return Ok(recordsList);
        }


    }
}