namespace WeatherForecastData.Models
{
    public abstract class WeatherBase
    {
        public int InnerWeatherId { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public int Pressure { get; set; }
        public float Humidity { get; set; }
        public float Temp { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public long DateTime { get; set; }

        public string WindSpeed { get; set; }
        public float WindDeg { get; set; }
    }
}
