using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class HourlyDaysWeather : WeatherBase
    {
        [JsonIgnore]
        public int HourlyDaysWeatherId { get; set; }

        [JsonIgnore]
        public int CityId { get; set; }

        [JsonIgnore]
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
