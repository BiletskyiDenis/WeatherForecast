using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecastHttp.Models
{
   public class SearchResult
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string Icon { get; set; }
        public float Temp { get; set; }
    }

}
