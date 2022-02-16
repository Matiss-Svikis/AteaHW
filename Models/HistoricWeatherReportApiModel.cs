namespace MatissHW.Models
{
    public class HistoricWeatherReportApiModel
    {
        public Location Location { get; set; }
        public Forecast Forecast { get; set; }
    }

    public class Hour
    {
        public int Time_epoch { get; set; }
        public double Temp_c { get; set; }
        public double Wind_kph { get; set; }
        public int Wind_degree { get; set; }
        public string  Wind_dir { get; set; }
    }

    public class Forecastday
    {
        public List<Hour> hour { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> forecastday { get; set; }
    }
}
