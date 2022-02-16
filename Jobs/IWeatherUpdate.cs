namespace MatissHW.Jobs
{
    public interface IWeatherUpdate
    {
        Task FetchWeatherReport();
        Task InitializeHistoricWeatherData();
    }
}