using Microsoft.EntityFrameworkCore;
using WeatherForecastData.Models;


namespace WeatherForecastData
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext(DbContextOptions options) : base(options)
        {
          
        }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CurrentDayWeather> CurrentDayWeathers { get; set; }
        public DbSet<HourlyDaysWeather> HourlyDaysWeathers { get; set; }
        public DbSet<WeekWeather> WeekWeather { get; set; }
    }
}
