﻿using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string key = "b0f6583a877d6f9d042e76e575470acb";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + ",&appid=" + key;


            var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = DateTime.Now;
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