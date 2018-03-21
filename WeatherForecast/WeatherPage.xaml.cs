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
    /// <summary>
    /// A page to view forecasts
    /// </summary>
    /// 

    //public ObservableCollection<Daylist> daylists { get; set; }

    public sealed partial class WeatherPage : Page
    {

        Forecast myForecast;
        public String SearchCrit { get; set; }
        public String cityCode { get; set; }
        
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

        // trying to get variable from mainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
             this.cityCode = e.Parameter.ToString();
            myForecast = new Forecast();
             BuildweatherAsync(cityCode);
        }

        public async Task BuildweatherAsync(String cityCode)
        {
            //DEBUG 
            Debug.WriteLine("DEBUG : BuildWeatherAsync"+ " citycode:"+cityCode);

            await myForecast.GetWeather(cityCode);//.GetAwaiter().GetResult();
            // 2965767
            //wait myForecast.GetWeather("id=2147714");//.GetAwaiter().GetResult();

            // CURRENT METHOD OF ADDING PIVOT
            PivotItem pvt;

            ScrollViewer
                     // Define a ScrollViewer
                     scroll = new ScrollViewer
                     {
                         VerticalScrollBarVisibility = ScrollBarVisibility.Visible
                     };

            //loop through SortedDays to seperate Day and hour forecasts 
            int xCount = 0, yCount = 0;
            foreach (var sd in myForecast.SortedDays)
            {

                pvt = new PivotItem
                {
                    Header = myForecast.SortedDays[xCount][0].dayOfWeek + "\n" + Convert.ToDateTime(myForecast.SortedDays[xCount][0].dtime).ToString("MMM d"),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                //    var dayStack = new StackPanel();
                Grid grid = new Grid();
                ListView listView1 = new ListView
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                foreach (var sh in sd)
                {
                    var hourStack = new StackPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center

                    };

                    var timeBlock = new TextBlock
                    {
                        Text = myForecast.SortedDays[xCount][yCount].dtime,
                        FontSize = 30,
                    };
                    hourStack.Children.Add(timeBlock);

                    var tempBlock = new TextBlock
                    {
                        Text = "Tempeture (c)\t:" + System.Convert.ToString(Math.Truncate((myForecast.SortedDays[xCount][yCount].temp - 273.15) * 100) / 100)
                    };
                    hourStack.Children.Add(tempBlock);

                    var descBlock = new TextBlock
                    {
                        Text = myForecast.SortedDays[xCount][yCount].desc
                    };
                    hourStack.Children.Add(descBlock);

                    var windBlock = new TextBlock
                    {
                        Text = "Windspeed\t:" + System.Convert.ToString(myForecast.SortedDays[xCount][yCount].windSpeed)
                    };
                    hourStack.Children.Add(windBlock);

                    var humBlock = new TextBlock
                    {
                        Text = "Humidity\t:" + System.Convert.ToString(myForecast.SortedDays[xCount][yCount].humidity)
                    };
                    hourStack.Children.Add(humBlock);

                    // append hourStack to dayStack
                    yCount++;
                    //dayStack.Children.Add(hourStack);
                    listView1.Items.Add(hourStack);
                }

                // set dayStack as pivots content
                pvt.Content = listView1;

                // add pivotItem to pivot
                pvtWeather.Items.Add(pvt);

                pvt = null;
                xCount++;
                yCount = 0;
            }
            /////////////////////////// TESTING NEW METHOD TO ADD PIVOT
/*
            daylists = new ObservableCollection<Daylist>
    {
        new Daylist {Day="Wednesday" ,Temperatures= new ObservableCollection<TimeTemperature>
        {
            new TimeTemperature {currenttime="2018-3-14 00:00:00",temperature="7.72",winSpeed="11.67" ,humidity=".95"},
            new TimeTemperature {currenttime="2018-3-14 01:00:00",temperature="7.72",winSpeed="11.67" ,humidity=".95" },
            new TimeTemperature {currenttime="2018-3-14 02:20:00",temperature="7.72",winSpeed="11.67" ,humidity=".95"}
            ...
        }},
        new Daylist {Day="Friday" ,Temperatures= new ObservableCollection<TimeTemperature>
        {
             new TimeTemperature {currenttime="2018-3-14 00:00:00",temperature="7.72",winSpeed="11.67" ,humidity=".95" }
        }}
    };
*/
        }

        // button to return to main menu
        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
    //// trying new way
    public class Daylist
    {
        public string Day { get; set; }
        public ObservableCollection<TimeTemperature> Temperatures { get; set; }
    }

    public class TimeTemperature
    {
        public string currenttime { get; set; }
        public string description { get; set; }
        public string winSpeed { get; set; }
        public string humidity { get; set; }
        public string temperature { get; set; }
    }

}
