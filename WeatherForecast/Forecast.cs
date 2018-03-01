using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Windows.Web.Http;//HttpClient;
using System.Diagnostics;
using Newtonsoft.Json;
//using System.Net.Http.HttpClient;
namespace WeatherForecast
{
    class Forecast
    {
        /*
         GetWeather initilizes a result onject with all of the selected counties data
         */
        public List<List<WeatherController>> sortedDays { get; set; }

        public async void GetWeather(string cCode)
        {
            string cityCode = cCode;
            string apiKey = "&APPID=833dac87e9be3b3f86533d84b6064a84";
            // string cityCode = "id=2964179";
            string url = "http://api.openweathermap.org/data/2.5/forecast?" + cityCode + apiKey;

            // adapted from https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            var uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    using (IHttpContent content = response.Content)
                    {
                        var json = await content.ReadAsStringAsync();
                        // adapted from https://stackoverflow.com/questions/36516146/parsing-json-in-uwp
                        var result = JsonConvert.DeserializeObject<RootObject>(json);

                        // create a list of weatherController lists to hold each day
                        // made public for global access
                        //  List<List<WeatherController>> sortedDays = new List<List<WeatherController>>();
                        sortedDays = new List<List<WeatherController>>();

                        //create a list of weatherController objects to hold each hourly interval
                        List<WeatherController> sortedHours = new List<WeatherController>();

                        // a base time
                        DateTime prevDate = Convert.ToDateTime("2000-01-01");
                        int counter = 0;

                        // iterate through result list  
                        for (int i = 0; i < result.list.Count(); i++)
                        {
                            // if the date is greater than the previous date add the sortedHours to sortedDays
                            if (Convert.ToDateTime(result.list[counter].dt_txt).Date > prevDate.Date && counter != 0)
                            {
                                sortedDays.Add(sortedHours);
                                sortedHours = new List<WeatherController>();
                            }
                            WeatherController wController = new WeatherController
                            {
                                dtime = result.list[counter].dt_txt,
                                dayOfWeek = (Convert.ToDateTime(result.list[counter].dt_txt).DayOfWeek).ToString(),
                                temp = result.list[counter].main.temp,
                                humidity = result.list[counter].main.humidity,
                                desc = result.list[counter].weather[0].description,
                                windSpeed = result.list[counter].wind.speed
                            };
                            sortedHours.Add(wController);

                            prevDate = Convert.ToDateTime(result.list[counter].dt_txt);
                            counter++;

                        }
                        // add any left over sortedHours to sortedDays
                        if (sortedHours != null)
                        {
                            sortedDays.Add(sortedHours);
                        }


                        // test List of list Structure
                        int xCount = 0, yCount = 0;
                        foreach (var sd in sortedDays)
                        {
                            foreach (var sh in sd)
                            {
                                // DEBUG
                                Debug.WriteLine("DEBUG: " + sortedDays[xCount][yCount].ToString());
                                yCount++;
                            }
                            Debug.WriteLine(" -");
                            xCount++;
                            yCount = 0;
                        }
                    }
                }
            }
        }


    }
}
