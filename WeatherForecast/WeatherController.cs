using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    class WeatherController
    {
        public String dtime { get; set; }
        public String dayOfWeek { get; set; }
        public double temp { get; set; }
        public int humidity { get; set; }
        public String desc { get; set; }
        public double windSpeed { get; set; }
        public String icon { get; set; }
        public String city { get; set; }

        public WeatherController()
        {

        }
        override
            public String ToString()
        {
            return "DAY:" + dayOfWeek + " dtime:" + dtime + " temp:" + temp + " humidity:" + humidity + " desc:" + desc + " windpeed:" + windSpeed;
        }
    }
}
