using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class CurrentDayWeather: WeatherBase
    {
        public int CurrentDayWeatherId { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
