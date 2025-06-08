using Microsoft.EntityFrameworkCore;
using System;
using WeatherDataApp;
using Xunit;

namespace WeatherDataUnitTest
{
    public class UnitTest1
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;

            return new AppDbContext(options);
        }

        private async Task SetTestData(AppDbContext context)
        {
            WeatherDataResponse weatherDataResponse = new WeatherDataResponse
            {
                latitude = 12.34,
                longitude = 56.78,
                hourly =
                {
                    time = new List<string>
                    {
                        "2025-06-08T00:00", "2025-06-08T01:00", "2025-06-08T02:00", "2025-06-08T03:00",
                    },
                    temperature_2m = new List<double>
                    {
                        56.6, 56.0, 56.5, 56.7
                    }
                }
            };
            context.WeatherData.Add(new WeatherDataResponse { latitude = 12.34, longitude = 56.78, hourly =  });
            await context.SaveChangesAsync();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}