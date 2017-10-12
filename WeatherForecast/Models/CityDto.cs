using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastData.Models;

namespace WeatherForecast.Models
{
    public class CityDto
    {
        public int CityWeatherId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }

        public CurrentDayWeatherDto CurrentDay { get; set; }
        public IEnumerable<HourlyDaysWeatherDto> DaysOfHourly { get; set; }
        public IEnumerable<WeekWeatherDto> DaysOfWeek { get; set; }
    }
}
