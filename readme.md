## How to setup
- install .net6 sdk and vs2022
- Make sure you have sql server running
- paste your sql connection string into appsettings.json
- open package manager console and run
```
update-database
```
- Then simply run it
- For convenience I left my weather api key in there, aswell as default db connection string.
## How it works
- In program.cs I run InitializeHistoricWeatherData to fetch all data about today (data here is hourly)
- Then a reccuring job(FetchWeatherReport) with Hangfire is setup to run every 15 minutes (You can access the dashboard by adding "/dashboard" to the root url)
- When user opens root page, the data for the last 24 hours is loaded from the database
- Then the graphs and calculations are done on the frontend using Chart.js library .