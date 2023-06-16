using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;

namespace WeatherApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
           var apiKeyObject =  File.ReadAllText("appsettings.json");
            var apiKey = JObject.Parse(apiKeyObject).GetValue("ApiKey").ToString();

            Console.WriteLine("Enter a city name to get the current weather forecast: ");
            string city = Console.ReadLine();

            string weatherURL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=imperial&appid={apiKey}";

            try
            {
                string weatherResponse = client.GetStringAsync(weatherURL).Result;
                JObject weatherJson = JObject.Parse(weatherResponse);
                Console.WriteLine("---------------------------");

                string cityName = weatherJson["name"].ToString();
                Console.WriteLine($"City: {cityName}");

                JToken mainWeather = weatherJson["weather"]?[0];
                string weatherDescription = mainWeather?["description"].ToString();
                Console.WriteLine($"Weather: {weatherDescription}");

                JToken mainInfo = weatherJson["main"];
                double temperature = Convert.ToDouble(mainInfo?["temp"].ToString());
                Console.WriteLine($"Temperature: {temperature}°F");

                JToken windInfo = weatherJson["wind"];
                double windSpeed = Convert.ToDouble(windInfo?["speed"].ToString());
                Console.WriteLine($"Wind Speed: {windSpeed} mph");

                Console.WriteLine();
                Console.WriteLine("Thank you for using the Weather Forecast App!");
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching the weather forecast.");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}