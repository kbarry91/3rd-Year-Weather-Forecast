using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Windows.Web.Http;//HttpClient;
//using System.Net.Http.HttpClient;
namespace WeatherForecast
{
    class Forecast
    {
        public  static void getWeather()
        {
            // adapted from https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            var client = new System.Net.HttpClient();
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                // Now parse with JSON.Net
            }
            using (HttpClient wc = new HttpClient())
            {
                //id=2964179 city code for galway
               // APPID = 833dac87e9be3b3f86533d84b6064a84 key for api
                  var json = wc.DownloadString("http://api.openweathermap.org/data/2.5/weather?id=2964179&APPID=833dac87e9be3b3f86533d84b6064a84");
            }
        }//getWeather
        public async Task AsyncMethod()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?id=2964179&APPID=833dac87e9be3b3f86533d84b6064a84";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = await content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
