using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecastData;
using WeatherForecastData.Models;
using WeatherForecastHttp;
using WeatherForecastService.Models;

namespace WeatherForecastService
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherForecastContext context;
        private readonly string appid;
        private readonly HttpWeather weather;
        private readonly int updateInterval; //update interval (minutes)

        public WeatherService(WeatherForecastContext cotext, IOptions<WeatherServiceSettings> settings)
        {
            this.context = cotext;
            this.weather = new CurrentWeather(appid);
            this.updateInterval = settings.Value.UpdateInterval;
            this.appid = settings.Value.Appid;
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

        public IEnumerable<HourlyDaysWeather> GetHourlyDayData(int id)
        {
            var data = weather.GetForecastDataById(id);

            var list = new List<HourlyDaysWeather>();

            foreach (var item in data.list)
            {
                var weatherData = new HourlyDaysWeather()
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

        public City GetFullCityData(int id)
        {
            var data = weather.GetWeatherDataById(id);
            var baseInfo = GetCityBase(data);
            var currentDayInfo = GeCurrentDayWeather(data);
            var hourlyDayInfo = GetHourlyDayData(id);
            var weekInfo = GetWeekData(id);


            var city = new City();
            return new City()
            {
                CityWeatherId = id,
                DaysOfHourly = hourlyDayInfo,
                CurrentDay = currentDayInfo,
                DaysOfWeek = weekInfo,
                CityName = baseInfo.CityName,
                Country = baseInfo.Country,
                UpateTime = DateTime.Now
            };
        }

        private IEnumerable<WeekWeather> GetWeekData(int id)
        {
            var data = weather.GetWeekData(id);
            var list = new List<WeekWeather>();

            foreach (var day in data.list)
            {
                list.Add(new WeekWeather()
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

        public void AddCity(int id)
        {
            var city = GetFullCityData(id);
            var user = GetSingleUser();
            city.UserData = user;
            this.context.Cities.Add(city);
            this.context.SaveChanges();
        }

        public IEnumerable<City> GetAll()
        {
            var user = GetSingleUser();

            var cities = this.context.Cities.Where(c => c.UserData == user)
                .Include(c => c.CurrentDay)
                .Include(c => c.DaysOfHourly)
                .Include(c => c.DaysOfWeek).ToList();

            if (cities == null && cities.Count() == 0)
            {
                return null;
            }

            foreach (var city in cities)
            {
                city.DaysOfHourly = city.DaysOfHourly.OrderBy(x => x.DateTime);
                city.DaysOfWeek = city.DaysOfWeek.OrderBy(x => x.DateTime);
            }

            return cities;
        }

        public int GetCount()
        {
            var user = GetSingleUser();
            var cities = this.context.Cities.Where(c => c.UserData == user).Count();
            return cities;
        }

        private CurrentDayWeather GeCurrentDayWeather(dynamic data)
        {
            return new CurrentDayWeather()
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

        private UserData GetSingleUser()
        {
            var users = this.context.UserDatas;
            if (users.Count() == 0)
            {
                var usd = new UserData()
                {
                    Name = "default",
                    SelectedId = 0,
                    Cities = new HashSet<City>()
                };

                users.Add(usd);
                this.context.SaveChanges();
            }
            return users.FirstOrDefault();
        }

        public int GetSelectedId()
        {
            return GetSingleUser().SelectedId;
        }

        public void SetSelectedId(int id)
        {
            var user = GetSingleUser();
            user.SelectedId = id;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCity(int id)
        {
            var user = GetSingleUser();
            var city = this.context.Cities.Where(c => c.UserData == user&& c.CityWeatherId == id).FirstOrDefault();
            if (city == null)
                return;

            if (user.SelectedId == id)
            {
                user.SelectedId = this.context.Cities.Where(c => c.UserData == user)
                    .FirstOrDefault()
                    .CityWeatherId;
            }
            else
            {
                user.SelectedId = 0;
            }

            this.context.Entry(city).State = EntityState.Deleted;
            this.context.SaveChanges();
        }

        public bool CheckForUpdate()
        {
            var updated = false;
            var user = GetSingleUser();
            var cities = this.context.Cities.Where(c => c.UserData == user)
                .Include(c => c.CurrentDay)
                .Include(c => c.DaysOfHourly)
                .Include(c => c.DaysOfWeek);

            foreach (var city in cities)
            {
                if (Math.Abs((DateTime.Now - city.UpateTime).TotalMinutes) > updateInterval)
                {
                    UpdateCityData(city);
                    updated = true;
                }
            }

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
            return updated;
        }

        private void UpdateCityData(City city)
        {
            var updateCity = GetFullCityData(city.CityWeatherId);
            city.UpateTime = DateTime.Now;

            city.CurrentDay = updateCity.CurrentDay;
            city.DaysOfHourly = updateCity.DaysOfHourly;
            city.DaysOfWeek = updateCity.DaysOfWeek;

            context.Entry(city).State = EntityState.Modified;
        }

        public CityBase GetCityBase(dynamic data)
        {
            return new CityBase()
            {
                CityWeatherId = data.id,
                CityName = data.name,
                Country = data.sys.country
            };
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
