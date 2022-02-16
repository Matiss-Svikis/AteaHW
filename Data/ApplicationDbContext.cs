using MatissHW.Models;
using Microsoft.EntityFrameworkCore;

namespace MatissHW.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<WeatherReport> WeatherReport { get; set; }
    }
}
