using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExample
{
    class WeatherInfo
    {
        // this class is used to deserialize a weather json data
        // the c# properties corresponds to the json token
        public DateTime Sunrise { set; get; }
        public DateTime Sunset { set; get; }
        public string CountryName { set; get; }
        public int GmtOffset { set; get; }
        public string TimezoneId { set; get; }

        public double DayDuration()
        {
            // substract Sunset and Sunrise time
            TimeSpan ts = Sunset.Subtract(Sunrise);
            // givs the difference between Sunset and Sunrise in hours
            return ts.TotalHours;
        }

        public void PrintJetLag()
        {
            // to be completed
	    // must print the jet lag between Paris and CountryName
        }
    }
}
