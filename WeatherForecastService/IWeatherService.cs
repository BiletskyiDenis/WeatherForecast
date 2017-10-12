using System.Collections.Generic;
using WeatherForecastData.Models;
using WeatherForecastHttp.Models;

namespace WeatherForecastService
{
    public interface IWeatherService
    {
        void AddCity(int id);
        bool CheckForUpdate();
        void DeleteCity(int id);
        void Dispose();
        IEnumerable<City> GetAll();
        int GetCount();
        int GetSelectedId();
        IEnumerable<SearchResult> Search(string name);
        void SetSelectedId(int id);
    }
}