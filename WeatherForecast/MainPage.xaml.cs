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
            
            (new Forecast()).GetWeather();

            string idate = "2018-03-05 15:00:00";
            string idate2 = "2018-03-02";
            DateTime oDate = Convert.ToDateTime(idate);
            DateTime oDate2 = Convert.ToDateTime(idate2);
            Debug.WriteLine("forecastparsed");

            if (oDate < oDate2)
            {
                Debug.WriteLine(oDate.Day + " " + oDate.Month + "  " + oDate.Year+" is first");
            }
            else
            {
                Debug.WriteLine(oDate2.Day + " " + oDate2.Month + "  " + oDate2.Year+"is first");
            }
            Debug.WriteLine(oDate.Day + " " + oDate.Month + "  " + oDate.Year);
        }
    }
}
