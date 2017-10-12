using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class WeekWeather: WeatherBase
    {
        public int WeekWeatherId { get; set; }

        public float TempNight { get; set; }
        public float TempEve { get; set; }
        public float TempMorn { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
