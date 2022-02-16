using System.ComponentModel.DataAnnotations;

namespace MatissHW.Models
{
    public class WeatherApiModel
    {
        [Key]
        public int id { get; set; }
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
    public class Location
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public int Localtime_epoch { get; set; }
        public string Localtime { get; set; }
    }

    public class Current
    {
        [Key]
        public int id { get; set; }
        public int Last_updated_epoch { get; set; }
        public string Last_updated { get; set; }
        public double Temp_c { get; set; }
        public double Wind_kph { get; set; }
        public int Wind_degree { get; set; }
        public string Wind_dir { get; set; }
    }



}









