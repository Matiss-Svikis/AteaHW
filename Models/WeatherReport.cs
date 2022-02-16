using System.ComponentModel.DataAnnotations;

namespace MatissHW.Models
{
    public class WeatherReport
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public int Last_updated_epoch { get; set; }
        public double Temp_c { get; set; }
        public double Wind_kph { get; set; }
        public int Wind_degree { get; set; }
        public string Wind_dir { get; set; }
    }
}
