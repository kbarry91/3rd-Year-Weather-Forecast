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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherForecast
{

    //public ObservableCollection<Daylist> daylists { get; set; }

    public sealed partial class WeatherPage : Page
    {

        Forecast myForecast;
        public String SearchCrit { get; set; }
        public String cityCode { get; set; }
        public String cityName { get; set; }

        public WeatherPage()
        {
            String cityCode;
            Debug.WriteLine("DEBUG : WeatherPage");
            this.InitializeComponent();

            //myForecast = new Forecast();
            // BuildweatherAsync(cityCode);

            //myForecast.GetWeather("id=2964179");//.GetAwaiter().GetResult();
            Debug.WriteLine("DEBUG IN WEATHERPAGE MAIN METHOD: ");
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
            //DEBUG 
            Debug.WriteLine("DEBUG : BuildWeatherAsync" + " citycode:" + cityCode);

            await myForecast.GetWeather(cityCode);//.GetAwaiter().GetResult();
                                                  // 2965767
                                                  //wait myForecast.GetWeather("id=2147714");//.GetAwaiter().GetResult();



            if (myForecast.httpSuccess)
            {
                this.cityName = myForecast.SortedDays[0][0].city;
            }
            else
            {
                this.cityName = cityName + " not found !";
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


        }

        // button to return to main menu
        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }


}
