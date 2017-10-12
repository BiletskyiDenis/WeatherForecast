using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastHttp.Models
{
    public class CurrentDayWeatherDto: WeatherBaseDto
    {
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}
