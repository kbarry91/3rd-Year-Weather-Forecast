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

            //myForecast.GetWeather("id=2964179");//.GetAwaiter().GetResult();
            Debug.WriteLine("DEBUG IN WEATHERPAGE MAIN METHOD: ");
        }
        public async Task BuildweatherAsync()
        {
            //DEBUG 
            Debug.WriteLine("DEBUG : BuildWeatherAsync");

            await myForecast.GetWeather("id=2964179");//.GetAwaiter().GetResult();


            //dynamically add a pivot item
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
                    Header = myForecast.SortedDays[xCount][0].dayOfWeek
                    
                };
            //    var dayStack = new StackPanel();
                ListView listView1 = new ListView();
                listView1.HorizontalContentAlignment = HorizontalAlignment.Center;
                foreach (var sh in sd)
                {
                    var hourStack = new StackPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center

                    };

                    var timeBlock = new TextBlock
                    {
                        Text = myForecast.SortedDays[xCount][yCount].dtime,
                        FontSize = 30
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
            
           
        }

    }

}
