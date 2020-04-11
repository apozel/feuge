using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading;

namespace WeatherWebService

{
    // example to access a web service that sent json data
    class Program
    {
        static void Main(string[] args)
        {
            // you can use the following keys to acces the web service
            String key1 = "327f93f0117f1f0009c3a14ee69ff283";
            String key2 = "06911ed047ce8e33a3bf3511fcc05974";

            // url to request the weather in Avignon
            string url1 = "http://api.openweathermap.org/data/2.5/weather?q=Avignon,fr&appid="+key1;
           
            // create a web  Client
            // A C#/.Net WebClient provides common methods for sending data to and receiving 
            // data from a resource identified by a URL
            WebClient client = new WebClient();
            // download the data
            string data = client.DownloadString(url1);
            Console.WriteLine("connecting openweathermap.org");
            // wait a bit for the server to have the time to send its answer
            Thread.Sleep(300);

            // show information of the data (json) that have been sent
            JObject jso = JObject.Parse(data);
            Console.WriteLine("visibility " + jso.SelectToken("visibility"));
            Console.WriteLine("coord lat " + jso.SelectToken("coord.lat"));
            Console.WriteLine("coord long " + jso.SelectToken("coord.lon"));
            Console.WriteLine("weather main " + jso.SelectToken("weather[0].main"));

            // geographical web service
            Console.WriteLine("\nconnecting geoname");
            // to be completed
            // connect to "http://api.geonames.org/astergdem
            

            // the end
            Console.WriteLine("press a key to exit");
            Console.ReadKey();
        }
    }
}
