using MatissHW.Data;
using MatissHW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatissHW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            TimeSpan t= DateTime.UtcNow -new DateTime(1970, 1, 1);
            List<WeatherReport> objCategoryList = _db.WeatherReport.Where(x=>x.Last_updated_epoch>=(int)t.TotalSeconds- 86400).ToList(); //take last 24 hours
            return View(objCategoryList);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}