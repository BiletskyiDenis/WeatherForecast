using System;
using System.Collections.Generic;

namespace WeatherForecastHttp.Models
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
