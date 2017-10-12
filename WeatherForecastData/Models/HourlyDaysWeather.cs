using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class HourlyDaysWeather : WeatherBase
    {
        public int HourlyDaysWeatherId { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
