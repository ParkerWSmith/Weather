using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            //Info for open weather
            string key = "b0f6583a877d6f9d042e76e575470acb";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + "&units=imperial&appid=" + key;

            var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                //Differnt weather information
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];


                //Time info for sunrise/sunset
                DateTime time = new System.DateTime(1970, 1, 1, 19, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " CST";
                weather.Sunset = sunset.ToString() + " CST";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}