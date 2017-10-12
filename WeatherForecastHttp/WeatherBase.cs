namespace WeatherForecastHttp
{
    public abstract class HttpWeather
    {
        protected string appid;

        public abstract dynamic GetWeatherDataByName(string name);
        public abstract dynamic GetWeatherDataById(int id);
        public abstract dynamic GetForecastDataById(int id);
        public abstract dynamic GetWeekData(int id);
        public abstract dynamic GetSearchResult(string name);


        public HttpWeather(string appid)
        {
            this.appid = appid;
        }
    }
}