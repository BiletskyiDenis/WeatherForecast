using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecastHttp.Models
{
    public class CityBase
    {
        public int CityWeatherId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}
