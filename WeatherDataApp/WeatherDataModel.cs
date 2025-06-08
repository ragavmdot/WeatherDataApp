using System.ComponentModel.DataAnnotations;

namespace WeatherDataApp
{
    public class WeatherDataModel
    {
        [Key]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string WeatherData { get; set; }
    }

    public class WeatherDataRequest
    {
        [Required(ErrorMessage = "Enter latitude data")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Enter longitude data")]
        public double Longitude { get; set; }
    }

    public class WeatherDataResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }

        public static implicit operator WeatherDataResponse(WeatherDataResponse v)
        {
            throw new NotImplementedException();
        }
    }

    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
    }
}
