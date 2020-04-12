using System;

class WeatherInfo
{
    // this class is used to deserialize a weather json data
    // the c# properties corresponds to the json token
    public DateTime Sunrise { set; get; }
    public String CountryCode { set; get; }

    public DateTime Sunset { set; get; }
    public string CountryName { set; get; }
    public int GmtOffset { set; get; }

    public int RawOffset { set; get; }
    public string TimezoneId { set; get; }
    public float Lat { set; get; }
    public float Lng { set; get; }
    public int DstOffset { set; get; }
    public DateTime Time { set; get; }

    public double DayDuration()
    {
        // substract Sunset and Sunrise time
        TimeSpan ts = Sunset.Subtract(Sunrise);
        // givs the difference between Sunset and Sunrise in hours
        return ts.TotalHours;
    }



    public int GetJetlagWithParis()
    {
        int ParisGmtOffset = 1;
        return GmtOffset - ParisGmtOffset;
    }

    public String PrintJetLag()
    {
        // must print the jet lag between Paris and CountryName
        return("coming from " + this.CountryName + ", the jetlag is " + this.GetJetlagWithParis() + " hours");
    }

    public override string ToString()
    {
        return string.Format("levee du jour : {0}, coucher : {1},jetlag : {2}",this.Sunrise,this.Sunset,this.PrintJetLag());
    }
}

