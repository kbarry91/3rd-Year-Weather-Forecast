// Author : G00339811
// Module : Mobile Application Developement 

using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;

namespace WeatherForecast
{

    /*
     * WeatherPage is the main weather page that displays all weather forecast data.
    */
    public sealed partial class WeatherPage : Page
    {
        Forecast myForecast;

        public String SearchCrit { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }

        public WeatherPage()
        {
            this.InitializeComponent();

        }

        // Recieve city name variable from mainPage.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.CityCode = e.Parameter.ToString();

            this.CityName = e.Parameter.ToString();
            myForecast = new Forecast();
            var buildWeather = BuildweatherAsync(CityCode);

        }


        /* BuildweatherAsync is a sync method that populates all the pivot items with the required data.
        *
        * param name="cityCode"
        * returns Task
        */
        public async Task BuildweatherAsync(String cityCode)
        {
            // Load the weather data.
            await myForecast.GetWeather(cityCode);

            // Set the appropriate header based on the http response.
            if (myForecast.httpSuccess)
            {
                this.CityName = myForecast.SortedDays[0][0].City;
            }
            else
            {
                this.CityName = "City not found Try Again\n  Or Search By Location";
            }
            cityBox.Text = CityName;

            // Populate pivot item with weather data and add to main pivot.
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
                    Header = myForecast.SortedDays[index++][0].DayOfWeek
                };

                ListView listView = new ListView
                {
                    ItemsSource = weathers
                };
                pivotItem.Content = listView;
                listView.ItemTemplate = ListViewDataTemplate;
                pvtWeather.Items.Add(pivotItem);

            }
            // Instansiate a new Pivot item to hold a map.
            var mapPivot = new PivotItem
            {
                Header = "Map"
            };

            // A list to hold points of interest.
            var poi = new List<MapElement>();

            // Retrieves the co-ordinates of the current forecast.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = myForecast.SortedDays[0][0].CoLat, Longitude = myForecast.SortedDays[0][0].CoLong };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Creates an icon for the map and adds the relevent weather icon.
            var locationIcon = new MapIcon
            {
                Location = cityCenter,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = "Today its looking like " + myForecast.SortedDays[0][0].Desc,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri(myForecast.SortedDays[0][0].Icon))
            };

            // Add the icon to the map.
            poi.Add(locationIcon);
            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = poi
            };

            // Create a new MapControl.
            MapControl weatherMap = new MapControl
            {
                // Set the map location.
                Center = cityCenter,
                ZoomLevel = 12,
                LandmarksVisible = true,

                ZoomInteractionMode = MapInteractionMode.GestureAndControl,
                TiltInteractionMode = MapInteractionMode.GestureAndControl,
                MapServiceToken = "iBE655yg4eGRRNKYjHUl~hnr8sRu5EvNBdY1r3_Sy3w~Ah7Zm5ZBjNR3W2DR4Canq2MolMsRbZiQB3UkoCeHdUo_l81X0c251rOzCH6TE-6Y"
            };
            weatherMap.Layers.Add(LandmarksLayer);

            // Center the map around the current location.
            weatherMap.Center = cityCenter;
            weatherMap.ZoomLevel = 14;
            mapPivot.Content = (weatherMap);

            // Add the map to the pivot.
            pvtWeather.Items.Add(mapPivot);
            pvtWeather.HorizontalAlignment = HorizontalAlignment.Center;

        }

        // Button to return to main menu.
        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void pvtWeather_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Play a sound effect.
                App.MyAppSounds.Play(SoundEfxEnum.SWIPE);
            }
            catch
            {
                Debug.WriteLine("Failed to play sound");
            }
        }
    }


}
