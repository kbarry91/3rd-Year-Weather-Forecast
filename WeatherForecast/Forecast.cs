﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Windows.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Windows.Storage;

namespace WeatherForecast
{
    class Forecast
    {
        public List<List<WeatherController>> SortedDays { get; set; }
        public RootObject Result { get; set; }
        private static HttpClient Client = new HttpClient();
        public Boolean httpSuccess;
        public Forecast()
        {

        }

        /*
         GetWeather initilizes a Result onject with all of the selected counties data
         */
        public async Task GetWeather(string cCode)
        {
            // load local storage
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // tempSetting represents the forma to convert kelvin to celsius
            double tempSetting= 273.15;

            // must confirm with local storage users selected temperature format
            try
            {

                switch ((string)localSettings.Values["tempSetting"])
                {
                    case "Celsius":
                        tempSetting = 273.15;
                        break;
                    case "Kelvin":
                        tempSetting = -0;
                        break;
                    default:
                        tempSetting = 273.15;
                        break;
                }
            }
            catch
            {
                // if no local storage value is available do not show the prevSearch textbox 
                tempSetting = 1;
            }
           
            string cityCode = cCode;
            string apiKey = "&APPID=833dac87e9be3b3f86533d84b6064a84";
           
            string url = "http://api.openweathermap.org/data/2.5/forecast?" + cityCode + apiKey;

            // adapted from https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            var uri = new Uri(url);
            
            using (HttpResponseMessage response = await Client.GetAsync(uri))
            {
                using (IHttpContent content = response.Content)
                {
                    // ensure that the httpRequest was a success
                    if (response.IsSuccessStatusCode)
                    {
                        this.httpSuccess = true;
                        Debug.WriteLine("DEBUG : content =" + content);
                        var json = await content.ReadAsStringAsync();
                        // adapted from https://stackoverflow.com/questions/36516146/parsing-json-in-uwp

                        Result = JsonConvert.DeserializeObject<RootObject>(json);
                        
                        SortedDays = new List<List<WeatherController>>();

                        //create a list of weatherController objects to hold each hourly interval
                        List<WeatherController> sortedHours = new List<WeatherController>();

                        // a base time
                        DateTime prevDate = Convert.ToDateTime("2000-01-01");
                        int counter = 0;

                        // iterate through Result list  
                        for (int i = 0; i < Result.list.Count(); i++)
                        {
                            // if the date is greater than the previous date add the sortedHours to SortedDays
                            if (Convert.ToDateTime(Result.list[counter].dt_txt).Date > prevDate.Date && counter != 0)
                            {
                                SortedDays.Add(sortedHours);
                                sortedHours = new List<WeatherController>();
                            }
                            WeatherController wController = new WeatherController
                            {
                                dtime = Result.list[counter].dt_txt,
                                dayOfWeek = (Convert.ToDateTime(Result.list[counter].dt_txt).DayOfWeek).ToString(),
                                temp = (Math.Truncate(Result.list[counter].main.temp - tempSetting) * 100) / 100,
                                humidity = Result.list[counter].main.humidity,
                                desc = Result.list[counter].weather[0].description,
                                windSpeed = Result.list[counter].wind.speed,
                                icon = "http://openweathermap.org/img/w/" + Result.list[counter].weather[0].icon + ".png",
                                city = Result.city.name,


                            coLong =Result.city.coord.lon,
                            coLat = Result.city.coord.lat
                        };
                           //Debug.WriteLine("DEBUG : lat =" + Result.coord.lat);
                            sortedHours.Add(wController);

                            prevDate = Convert.ToDateTime(Result.list[counter].dt_txt);
                            counter++;

                        }

                        // add any left over sortedHours to SortedDays
                        if (sortedHours != null)
                        {
                            SortedDays.Add(sortedHours);
                        }

                    }
                    else
                    {
                        this.httpSuccess = false;
                        Debug.WriteLine("DEBUG: response error"+response.ReasonPhrase);
                    }
                    
                    
                }
            }//using (HttpResponseMessage response = await Client.GetAsync(uri))

           
        }


    }
}
