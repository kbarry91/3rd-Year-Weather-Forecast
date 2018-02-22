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
using System.Net;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherForecast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Debug.WriteLine("Hello world");
            try
            {
               var result= Forecast.getWeather().Wait();
            }
            catch (Exception ex)
            {
                WriteLine($"There was an exception: {ex.ToString()}");
            }
            var task = TaskEx.RunEx(async () => await MyAsyncMethod());
            var result = task.WaitAndUnwrapException();
            var result = Forecast.Run(MyAsyncMethod);
            await Forecast.getWeather();
          //  var task = Forecast.getWeather();
            var result = task.WaitAndUnwrapException();
        }
    }
}
