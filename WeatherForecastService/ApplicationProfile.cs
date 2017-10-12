using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastData.Models;
using WeatherForecastHttp.Models;

namespace WeatherForecastService
{
    public class ApplicationProfile:AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CityDto,City >()
                .ReverseMap();

            CreateMap<CurrentDayWeatherDto, CurrentDayWeather>()
                .ReverseMap();

            CreateMap<HourlyDaysWeather, HourlyDaysWeatherDto>()
                .ReverseMap();

            CreateMap<WeekWeatherDto, WeekWeather>()
                .ReverseMap();
        }
    }
}
