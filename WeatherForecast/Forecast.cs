// Author : G00339811
// Module : Mobile Application Developement 

using System;
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
         GetWeather initilizes a Result onject with all of the selected countries data.
         */
        public async Task GetWeather(string cCode)
        {
            // Load local storage
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // TempSetting represents the forma to convert kelvin to celsius.
            double tempSetting = 273.15;

            // Must confirm with local storage users selected temperature format.
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
                // If no local storage value is available do not show the prevSearch textbox.
                tempSetting = 1;
            }

            string cityCode = cCode;
            string apiKey = "&APPID=833dac87e9be3b3f86533d84b6064a84";

            string url = "http://api.openweathermap.org/data/2.5/forecast?" + cityCode + apiKey;

            // Adapted from https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            var uri = new Uri(url);

            using (HttpResponseMessage response = await Client.GetAsync(uri))
            {
                using (IHttpContent content = response.Content)
                {
                    // Ensure that the httpRequest was a success.
                    if (response.IsSuccessStatusCode)
                    {
                        this.httpSuccess = true;
                        Debug.WriteLine("DEBUG : content =" + content);
                        var json = await content.ReadAsStringAsync();

                        // Adapted from https://stackoverflow.com/questions/36516146/parsing-json-in-uwp
    
                        Result = JsonConvert.DeserializeObject<RootObject>(json);

                        SortedDays = new List<List<WeatherController>>();

                        // Create a list of weatherController objects to hold each hourly interval.
                        List<WeatherController> sortedHours = new List<WeatherController>();

                        // A base time.
                        DateTime prevDate = Convert.ToDateTime("2000-01-01");
                        int counter = 0;

                        // iterate through Result list  .
                        for (int i = 0; i < Result.List.Count(); i++)
                        {
                            // if the date is greater than the previous date add the sortedHours to SortedDays.
                            if (Convert.ToDateTime(Result.List[counter].Dt_txt).Date > prevDate.Date && counter != 0)
                            {
                                SortedDays.Add(sortedHours);
                                sortedHours = new List<WeatherController>();
                            }
                            WeatherController wController = new WeatherController
                            {
                                Dtime = Result.List[counter].Dt_txt,
                                DayOfWeek = (Convert.ToDateTime(Result.List[counter].Dt_txt).DayOfWeek).ToString(),
                                Temp = (Math.Truncate(Result.List[counter].Main.Temp - tempSetting) * 100) / 100,
                                Humidity = Result.List[counter].Main.Humidity,
                                Desc = Result.List[counter].Weather[0].Description,
                                WindSpeed = Result.List[counter].Wind.Speed,
                                Icon = "http://openweathermap.org/img/w/" + Result.List[counter].Weather[0].Icon + ".png",
                                City = Result.City.Name,
                                CoLong = Result.City.Coord.Lon,
                                CoLat = Result.City.Coord.Lat
                            };

                            sortedHours.Add(wController);

                            prevDate = Convert.ToDateTime(Result.List[counter].Dt_txt);
                            counter++;
                        }

                        // Add any left over sortedHours to SortedDays.
                        if (sortedHours != null)
                        {
                            SortedDays.Add(sortedHours);
                        }

                    }
                    else
                    {
                        this.httpSuccess = false;
                        Debug.WriteLine("DEBUG: response error" + response.ReasonPhrase);
                    }
                               }
            }//using (HttpResponseMessage response = await Client.GetAsync(uri))
        }
    }
}
