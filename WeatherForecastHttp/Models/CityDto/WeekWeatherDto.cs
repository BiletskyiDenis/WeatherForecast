using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastHttp.Models
{
    public class WeekWeatherDto:WeatherBaseDto
    {
        public float TempNight { get; set; }
        public float TempEve { get; set; }
        public float TempMorn { get; set; }
    }
}
