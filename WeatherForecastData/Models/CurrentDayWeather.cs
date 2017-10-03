using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class CurrentDayWeather: WeatherBase
    {
        [JsonIgnore]
        public int CurrentDayWeatherId { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        [JsonIgnore]
        public int CityId { get; set; }

        [JsonIgnore]
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
