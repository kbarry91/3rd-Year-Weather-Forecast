using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherForecast;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherForecast
{
    /// <summary>
    /// A page to view forecasts
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        Forecast myForecast;
        public WeatherPage()
        {
            Debug.WriteLine("DEBUG : WeatherPage");
            this.InitializeComponent();

             myForecast = new Forecast();
             BuildweatherAsync();
            // Task task =myForecast.GetWeather("id=2964179");
            // Forecast myfor =  new Forecast().GetWeather("uyuy");
            //myForecast.GetWeather("id=2964179");//.GetAwaiter().GetResult();

            // Debug.WriteLine("DEBUG IN WEATHERPAGE: " + myForecast.SortedDays[1][1].desc);
        }
        public async Task BuildweatherAsync()
        {
            //DEBUG 
            Debug.WriteLine("DEBUG : BuildWeatherAsync");

            await myForecast.GetWeather("id=2964179");//.GetAwaiter().GetResult();

            Debug.WriteLine("DEBUG IN WEATHERPAGE: " + myForecast.SortedDays[4][2].desc);
        }
       
    }

}
