using System;
using System.Collections.Generic;
using System.Text;
using WeatherForecastData.Models;

namespace WeatherForecastService.Models
{
    public class DataContiner
    {
        public CurrentDayWeather CurrentDay { get; set; }
        public CityBase CityBase { get; set; }

        public DataContiner(CurrentDayWeather currentDay, CityBase cityBase)
        {
            this.CityBase = cityBase;
            this.CurrentDay = currentDay;
        } 
    }
}
