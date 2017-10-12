using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastData.Models
{
    public class City
    {
        public int CityId { get; set; }
        public int CityWeatherId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public virtual CurrentDayWeather CurrentDay { get; set; }
        public virtual IEnumerable<HourlyDaysWeather> DaysOfHourly { get; set; }
        public virtual IEnumerable<WeekWeather> DaysOfWeek { get; set; }
        public DateTime UpateTime { get; set; }
        [ForeignKey("UserDataId")]
        public UserData UserData { get; set; }
    }
}
