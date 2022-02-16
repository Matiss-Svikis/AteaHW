using MatissHW.Data;
using MatissHW.Models;
using System.Net.Http.Headers;

namespace MatissHW.Jobs
{
    public class WeatherUpdate : IWeatherUpdate
    {
        private readonly string[] cities = {"Osaka", "Tokyo", "Sweden", "Gothenburg", "Riga", "Liepaja","Washington", "San francisco", "Cairo", "Aswan" };
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient client = new();
        public WeatherUpdate(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task FetchWeatherReport()
        {
            foreach (var item in cities)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.weatherapi.com/v1/current.json?key=a8a858040d5e4496918204558221002&q={item}&aqi=no");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        WeatherApiModel weatherApiResponse = await response.Content.ReadAsAsync<WeatherApiModel>();
                        WeatherReport weatherReport = new() {
                            Region = weatherApiResponse.Location.Region,
                            Name = weatherApiResponse.Location.Name,
                            Country = weatherApiResponse.Location.Country,
                            Last_updated_epoch = weatherApiResponse.Current.Last_updated_epoch,
                            Temp_c = weatherApiResponse.Current.Temp_c,
                            Wind_kph = weatherApiResponse.Current.Wind_kph,
                            Wind_degree = weatherApiResponse.Current.Wind_degree,
                            Wind_dir = weatherApiResponse.Current.Wind_dir,
                        };
                        _db.WeatherReport.Add(weatherReport);
                        Console.WriteLine("A weather report, has been saved.");
                    }
                }              
            }
                    await _db.SaveChangesAsync();
        }
        public async Task InitializeHistoricWeatherData()
        {
            if (_db.WeatherReport.Any()) { return; }
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            foreach (var item in cities)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.weatherapi.com/v1/history.json?key=a8a858040d5e4496918204558221002&q={item}&dt={today}");
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    try
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            HistoricWeatherReportApiModel historicWeatherApiResponse = await response.Content.ReadAsAsync<HistoricWeatherReportApiModel>();
                            foreach (var forecastDay in historicWeatherApiResponse.Forecast.forecastday)
                            {
                                foreach (var report in forecastDay.hour)
                                {
                                    if(report.Time_epoch<= secondsSinceEpoch)
                                    {
                                        WeatherReport weatherReport = new()
                                        {
                                            Region = historicWeatherApiResponse.Location.Region,
                                            Name = historicWeatherApiResponse.Location.Name,
                                            Country = historicWeatherApiResponse.Location.Country,
                                            Last_updated_epoch = report.Time_epoch,
                                            Temp_c = report.Temp_c,
                                            Wind_kph = report.Wind_kph,
                                            Wind_degree = report.Wind_degree,
                                            Wind_dir = report.Wind_dir,
                                        };
                                        _db.WeatherReport.Add(weatherReport);
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
