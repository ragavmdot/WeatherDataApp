//using System;
//using System.Data.SqlClient;
//using System.Reflection.PortableExecutable;
//using System.Text.Json;

//namespace WeatherDataApp
//{
//    public class WeatherRepository : IWeatherRepository
//    {
//        private readonly string _connectionString;

//        public WeatherRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("SqlConnection");
//        }

//        public void AddWeatherData(WeatherDataResponse request)
//        {
//            //using (SqlConnection conn = new SqlConnection(_connectionString))
//            //{
//            //    string query = "INSERT INTO WeatherData (Latitude, Longitude, WeatherData) VALUES (@Latitude, @Longitude, @WeatherData)";
//            //    SqlCommand cmd = new SqlCommand(query, conn);

//            //    cmd.Parameters.AddWithValue("@Latitude", request.latitude);
//            //    cmd.Parameters.AddWithValue("@Longitude", request.longitude);
//            //    cmd.Parameters.AddWithValue("@WeatherData", JsonSerializer.Serialize(request));

//            //    conn.Open();
//            //    cmd.ExecuteNonQuery();
//            //    conn.Close();
//            //}

//            //Console.WriteLine("Weather data added successfully.");
//        }

//        public WeatherDataResponse GetWeatherData(WeatherDataResponse request)
//        {
//            //using (SqlConnection conn = new SqlConnection(_connectionString))
//            //{
//            //    string query = "SELECT Latitude, Longitude, WeatherData FROM dbo.WeatherData WHERE Latitude = @Latitude AND Longitude = @Longitude;";
//            //    SqlCommand cmd = new SqlCommand(query, conn);

//            //    cmd.Parameters.AddWithValue("@Latitude", request.latitude);
//            //    cmd.Parameters.AddWithValue("@Longitude", request.longitude);

//            //    conn.Open();
//            //    using var reader = cmd.ExecuteReader();
//            //    cmd.ExecuteReader();

//            //    if (reader.Read())
//            //    {
//            //        return new WeatherDataResponse
//            //        {
//            //            latitude = reader.GetInt32(0),
//            //            longitude = reader.GetString(1),
//            //            hourly = reader.GetInt32(2)
//            //        };
//            //    }
//            //    conn.Close();
//            //}

//            //Console.WriteLine("Weather data added successfully.");
//        }
//    }
//}
