using Microsoft.EntityFrameworkCore;
using System;

namespace WeatherDataApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherDataModel> WeatherData { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
