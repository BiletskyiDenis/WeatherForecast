using AutoMapper;
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
using WeatherForecastHttp.Models;
using WeatherForecastService.Models;

namespace WeatherForecastService
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherForecastContext context;
        private readonly IMapper mapper;
        private readonly HttpDataService httpDataService;
        private readonly int updateInterval; //update interval (minutes)

        public WeatherService(WeatherForecastContext cotext, IOptions<WeatherServiceSettings> settings)
        {
            this.context = cotext;
            this.httpDataService = new HttpDataService(settings.Value.Appid);
            this.updateInterval = settings.Value.UpdateInterval;

            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });
            mapper = config.CreateMapper();
        }

        public IEnumerable<SearchResult> Search(string name)
        {
            return httpDataService.Search(name);
        }

        public void AddCity(int id)
        {
            var cityDto = httpDataService.GetFullCityData(id);
            var city=mapper.Map<City>(cityDto);
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
            var updateCity = httpDataService.GetFullCityData(city.CityWeatherId);
            city.UpateTime = DateTime.Now;

            city.CurrentDay = mapper.Map<CurrentDayWeather>(updateCity.CurrentDay);
            city.DaysOfHourly = mapper.Map<IEnumerable<HourlyDaysWeather>>(updateCity.DaysOfHourly);
            city.DaysOfWeek = mapper.Map<IEnumerable<WeekWeather>>(updateCity.DaysOfWeek);

            context.Entry(city).State = EntityState.Modified;
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
