using System.Collections.Generic;

namespace WeatherForecastData.Models
{
    public class UserData
    {
        public UserData()
        {
            Cities = new HashSet<City>();
        }
        public int UserDataId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }
        public int SelectedId { get; set; }
    }
}