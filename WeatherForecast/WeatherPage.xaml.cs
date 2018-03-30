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
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;

namespace WeatherForecast
{


    public sealed partial class WeatherPage : Page
    {

        Forecast myForecast;
        public String SearchCrit { get; set; }
        public String cityCode { get; set; }
        public String cityName { get; set; }

        public WeatherPage()
        {
            this.InitializeComponent();
            
        }

        // Recieve city name variable from mainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            

            base.OnNavigatedTo(e);
           this.cityCode =  e.Parameter.ToString();
           
            this.cityName = e.Parameter.ToString();
            myForecast = new Forecast();
           var buildWeather = BuildweatherAsync(cityCode);
            
          
        }

        public async Task BuildweatherAsync(String cityCode)
        {
            
            await myForecast.GetWeather(cityCode);

            if (myForecast.httpSuccess)
            {
                this.cityName = myForecast.SortedDays[0][0].city;
            }
            else
            {
                this.cityName = "City not found Try Again\n  Or Search By Location";
            }                      
            
            cityBox.Text = cityName;
            int index = 0;
            foreach (var day in myForecast.SortedDays)
            { 
                var weathers = new ObservableCollection<WeatherController>();
                foreach (var weatherItem in day)
                {
                    weathers.Add(weatherItem);
                }

                var pivotItem = new PivotItem
                {
                    Header = myForecast.SortedDays[index++][0].dayOfWeek
                };
                ListView listView = new ListView
                {
                    ItemsSource = weathers
                };
                pivotItem.Content = listView;
                listView.ItemTemplate = ListViewDataTemplate;
                pvtWeather.Items.Add(pivotItem);

            }
            var mapPivot = new PivotItem
            {
                Header = "Map"
            };
            
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = myForecast.SortedDays[0][0].coLat, Longitude = myForecast.SortedDays[0][0].coLong };
            Geopoint cityCenter = new Geopoint(cityPosition);
             
            
            mapPivot.Content = "This is a map";
            MapControl MapControl2 = new MapControl();
            // Set the map location.
            MapControl2.Center = cityCenter;
            MapControl2.ZoomLevel = 12;
            MapControl2.LandmarksVisible = true;

            MapControl2.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.TiltInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.MapServiceToken = "As7Ns8nGzuBs50x2zsXt1nXd7kIxbsQkTdVMpv9z8VaRBfMgki0iCCKJnqRLfrjq";

            mapPivot.Content = (MapControl2);
           // pageGrid.Children.Add(MapControl2);
            pvtWeather.Items.Add(mapPivot);
            
        }

        // button to return to main menu
        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }


}
