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
        public DateTime sunrise { set; get; }
        public String countryCode { set; get; }

        public DateTime Sunset { set; get; }
        public string CountryName { set; get; }
        public int GmtOffset { set; get; }

        public int rawOffset { set; get; }
        public string TimezoneId { set; get; }
        public float lat { set; get; }
        public float lng { set; get; }
        public int dstOffset { set; get; }
        public DateTime time { set; get; }

        public double DayDuration()
        {
            // substract Sunset and Sunrise time
            TimeSpan ts = Sunset.Subtract(sunrise);
            // givs the difference between Sunset and Sunrise in hours
            return ts.TotalHours;
        }

       

        public int GetJetlagWithParis()
        { int ParisGmtOffset = 1;
            return GmtOffset - ParisGmtOffset;
        }

        public void PrintJetLag()
        {

            // must print the jet lag between Paris and CountryName
            Console.WriteLine("coming from " + this.CountryName + ", the jetlag is " + this.GetJetlagWithParis() + " hours");
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
