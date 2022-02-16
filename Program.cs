using Hangfire;
using MatissHW.Data;
using MatissHW.Jobs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString(name: "DefaultConnection")
    ));
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IWeatherUpdate, WeatherUpdate>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireDashboard("/dashboard");
var serviceProvider = app.Services.CreateScope().ServiceProvider;
serviceProvider.GetRequiredService<IWeatherUpdate>().InitializeHistoricWeatherData();

RecurringJob.AddOrUpdate(() => serviceProvider.GetRequiredService<IWeatherUpdate>().FetchWeatherReport(), "*/15 * * * *"); //every 15mins

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
