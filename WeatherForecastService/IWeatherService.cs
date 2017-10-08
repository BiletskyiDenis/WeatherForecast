using System;
using System.Collections.Generic;
using WeatherForecastData.Models;
using WeatherForecastService.Models;

namespace WeatherForecastService
{
    public interface IWeatherService
    {
        void AddCity(int id);
        bool CheckForUpdate();
        void DeleteCity(int id);
        void Dispose();
        IEnumerable<City> GetAll();
        CityBase GetCityBase(dynamic data);
        int GetCount();
        City GetFullCityData(int id);
        IEnumerable<HourlyDaysWeather> GetHourlyDayData(int id);
        int GetSelectedId();
        IEnumerable<SearchResult> Search(string name);
        void SetSelectedId(int id);
    }
}