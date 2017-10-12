using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Models;
using WeatherForecastData.Models;

namespace WeatherForecast
{
    public class ApplicationProfile:AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<City, CityDto>()
                .ReverseMap();

            CreateMap<CurrentDayWeather, CurrentDayWeatherDto>()
                .ReverseMap();

            CreateMap<HourlyDaysWeather, HourlyDaysWeatherDto>()
                .ReverseMap();

            CreateMap<WeekWeather, WeekWeatherDto>()
                .ReverseMap();
        }
    }
}
