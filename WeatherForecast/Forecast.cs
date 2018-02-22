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
        public async void GetWeather()
        {
             string url = "http://api.openweathermap.org/data/2.5/weather?id=2964179&APPID=833dac87e9be3b3f86533d84b6064a84";

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
                        //var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<RootObject>(json);

                        Debug.WriteLine("In async method");
                        Debug.WriteLine( result.ToString());
                        Debug.WriteLine(result+ "" +result.list);

                    }
                }
            }
        }
       
    }
}
