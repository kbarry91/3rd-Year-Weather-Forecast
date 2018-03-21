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
using Windows.Devices.Geolocation;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherForecast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       // var accessStatus = await Geolocator.RequestAccessAsync();

        public String userCity { get; set; }

        public MainPage()
        {
            //request permission for location
            this.InitializeComponent();
            DataContext = this;
            //string cityCode = "id=2964179";
            //(new Forecast()).GetWeather(cityCode);

            //DEBUG
            Debug.WriteLine("DEBUG : end of mainpage reached");
        }

        private void cityButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //DEBUG Debug.WriteLine("DEBUG : CityButton Tapped");
            String userText = cityInput.Text;
            Debug.WriteLine("DEBUG : User input =" + userText);

            string cityCode;
            if (userText.Equals("galway", StringComparison.CurrentCultureIgnoreCase))
            {
                  cityCode = "id=2964179";
                //cityCode = "galway";
                cityCode = "lat=53.333328&lon=-9";
                // (new Forecast()).GetWeather(cityCode);
                
                Frame.Navigate(typeof(WeatherPage),cityCode);
            }

        }
    }
}
