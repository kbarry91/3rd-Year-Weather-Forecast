using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    /*
     * WeatherController is used to simplify the weather details into a small lightweight object.
    */
    class WeatherController
    {
        public String Dtime { get; set; }
        public String DayOfWeek { get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public String Desc { get; set; }
        public double WindSpeed { get; set; }
        public String Icon { get; set; }
        public String City { get; set; }
        public double CoLong { get; set; }
        public double CoLat { get; set; }

        public WeatherController() { }

        override
            public String ToString()
        {
            return "DAY:" + DayOfWeek + " dtime:" + Dtime + " temp:" + Temp + " humidity:" + Humidity + " desc:" + Desc + " windpeed:" + WindSpeed;
        }
    }
}
