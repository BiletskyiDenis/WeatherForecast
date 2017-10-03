using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastService
{
    public abstract class WeatherBase
    {
        protected string appid;

        public abstract dynamic GetWeatherDataByName(string name);
        public abstract dynamic GetWeatherDataById(int id);
        public abstract dynamic GetForecastDataById(int id);
        public abstract dynamic GetWeekData(int id);
        public abstract dynamic GetSearchResult(string name);


        public WeatherBase(string appid)
        {
            this.appid = appid;
        }
    }

    public class CurrentWeather : WeatherBase
    {
        public override dynamic GetForecastDataById(int id)
        {

            var url = ($"http://api.openweathermap.org/data/2.5/forecast?id={id}&mode=json&units=metric&appid={appid}");
            return GetData(url).Result;
        }

        public override dynamic GetWeatherDataById(int id)
        {
            var url = ($"http://api.openweathermap.org/data/2.5/weather?id={id}&mode=json&units=metric&appid={appid}");
            return GetData(url).Result;
        }

        public override dynamic GetWeatherDataByName(string name)
        {
            var url = ($"http://api.openweathermap.org/data/2.5/weather?q={name}&mode=json&units=metric&appid={appid}");
            return GetData(url).Result;
        }
        public CurrentWeather(string appid) : base(appid)
        {

        }


        public override dynamic GetSearchResult(string name)
        {
            var url = ($"http://api.openweathermap.org/data/2.5/find?q={name}&type=like&sort=population&units=metric&cnt=30&appid={appid}");
            return GetData(url).Result;
        }

        public override dynamic GetWeekData(int id)
        {
            
            var url = ($"http://api.openweathermap.org/data/2.5/forecast/daily?id={id}&cnt=9&units=metric&appid={appid}");
            return GetData(url).Result;
        }

        private async Task<dynamic> GetData(string url)
        {
            var client = new HttpClient();
            var res = string.Empty;
            dynamic data;
            try
            {
                res = await client.GetStringAsync(url);
                data = Newtonsoft.Json.JsonConvert.DeserializeObject(res);
            }
            catch (Exception)
            {
                return null;
            }

            if (data == null || data.cod != "200")
            {
                return null;
            }
            return data;
        }

    }
}
