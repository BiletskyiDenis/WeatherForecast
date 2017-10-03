using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeatherForecastData.Models
{
    public class WeekWeather: WeatherBase
    {
        [JsonIgnore]
        public int WeekWeatherId { get; set; }

        public float TempNight { get; set; }
        public float TempEve { get; set; }
        public float TempMorn { get; set; }

        [JsonIgnore]
        public int CityId { get; set; }

        [JsonIgnore]
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
