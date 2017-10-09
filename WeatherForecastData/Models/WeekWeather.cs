using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class WeekWeather: WeatherBase
    {
        [JsonIgnore]
        public int WeekWeatherId { get; set; }

        public float TempNight { get; set; }
        public float TempEve { get; set; }
        public float TempMorn { get; set; }

        [JsonIgnore]
        public int CityId { get; set; }

        [JsonIgnore]
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
