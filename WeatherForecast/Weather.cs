﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }

        public override string ToString()
        {
            // converts tempeture from kelvin to degrees and truncates to 2 deciaml places
            var celsius = Math.Truncate((temp - 273.15) * 100) / 100;
            var str = " celsius" + celsius + " humidity" + humidity;
            return str;
        }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public override string ToString()
        {

            return description;
        }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }

    }
    public class Rain
    {
        public double? __invalid_name__3h { get; set; }
    }

    public class Snow
    {
        public double __invalid_name__3h { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Snow snow { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }

        public override string ToString()
        {

            var myStr = "";
            foreach (var myWeather in weather)
            {

                //  Debug.WriteLine(myWeather.ToString());
                myStr = myStr + main.ToString() + dt_txt + " " + myWeather.ToString();

            }
            return myStr + main.ToString();
        }
    }

    public class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
    }

    public class RootObject
    {
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
        public Coord coord { get; set; }

        public override string ToString()
        {
            var count = 0;
            var myStr = "";
            foreach (var mylist in list)
            {
                count++;
                myStr = myStr + mylist.ToString() + " c " + count + "\n";
                // Debug.WriteLine(mylist.ToString());
            }
            return myStr;
        }
    }
}
