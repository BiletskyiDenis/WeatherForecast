using System;
using System.Collections.Generic;
using System.Text;
using WeatherForecastHttp.Models;

namespace WeatherForecastHttp
{
    public class HttpDataService
    {
        private readonly HttpWeather weather;

        public HttpDataService(string appid)
        {
            this.weather = new CurrentWeather(appid);
        }
        public IEnumerable<SearchResult> Search(string name)
        {
            var searchResult = new List<SearchResult>();
            var data = weather.GetSearchResult(name);
            foreach (var item in data.list)
            {
                searchResult.Add(new SearchResult()
                {
                    CityName = item.name,
                    Country = item.sys.country,
                    Icon = item.weather[0].icon,
                    Id = item.id,
                    Temp = item.main.temp
                });
            }
            return searchResult;
        }

        private IEnumerable<WeekWeatherDto> GetWeekData(int id)
        {
            var data = weather.GetWeekData(id);
            var list = new List<WeekWeatherDto>();

            foreach (var day in data.list)
            {
                list.Add(new WeekWeatherDto()
                {
                    DateTime = day.dt,
                    Description = day.weather[0].description,
                    Main = day.weather[0].main,
                    InnerWeatherId = day.weather[0].id,
                    Icon = day.weather[0].icon,
                    Humidity = day.humidity,
                    Pressure = day.pressure,
                    Temp = day.temp.day,
                    TempMax = day.temp.max,
                    TempMin = day.temp.min,
                    TempEve = day.temp.eve,
                    TempMorn = day.temp.morn,
                    TempNight = day.temp.night,
                    WindDeg = day.deg,
                    WindSpeed = day.speed
                });
            }

            return list;
        }

        private CityBase GetCityBase(dynamic data)
        {
            return new CityBase()
            {
                CityWeatherId = data.id,
                CityName = data.name,
                Country = data.sys.country
            };
        }

        private CurrentDayWeatherDto GeCurrentDayWeather(dynamic data)
        {
            return new CurrentDayWeatherDto()
            {
                Icon = data.weather[0].icon,
                Temp = data.main.temp,
                TempMax = data.main.temp_max,
                TempMin = data.main.temp_min,
                Pressure = data.main.pressure,
                Humidity = data.main.humidity,
                Main = data.weather[0].main,
                DateTime = data.dt,
                Sunrise = data.sys.sunrise,
                Sunset = data.sys.sunset,
                Description = data.weather[0].description,
                InnerWeatherId = data.weather[0].id,
                WindDeg = data.wind.deg,
                WindSpeed = data.wind.speed
            };
        }

        private IEnumerable<HourlyDaysWeatherDto> GetHourlyDayData(int id)
        {
            var data = weather.GetForecastDataById(id);

            var list = new List<HourlyDaysWeatherDto>();

            foreach (var item in data.list)
            {
                var weatherData = new HourlyDaysWeatherDto()
                {
                    InnerWeatherId = item.weather[0].id,
                    Main = item.weather[0].main,
                    Description = item.weather[0].description,
                    Icon = item.weather[0].icon,
                    Temp = item.main.temp,
                    TempMax = item.main.temp_max,
                    TempMin = item.main.temp_min,
                    Pressure = item.main.pressure,
                    Humidity = item.main.humidity,
                    DateTime = item.dt,
                    WindDeg = item.wind.deg,
                    WindSpeed = item.wind.speed
                };
                list.Add(weatherData);
            }

            return list;
        }

        public CityDto GetFullCityData(int id)
        {
            var data = weather.GetWeatherDataById(id);
            var baseInfo = GetCityBase(data);
            var currentDayInfo = GeCurrentDayWeather(data);
            var hourlyDayInfo = GetHourlyDayData(id);
            var weekInfo = GetWeekData(id);


            var city = new CityDto();
            return new CityDto()
            {
                CityWeatherId = id,
                DaysOfHourly = hourlyDayInfo,
                CurrentDay = currentDayInfo,
                DaysOfWeek = weekInfo,
                CityName = baseInfo.CityName,
                Country = baseInfo.Country
                //UpateTime = DateTime.Now
            };
        }
    }
}
