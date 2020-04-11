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
            string url1 = "http://api.openweathermap.org/data/2.5/weather?q=Avignon,fr&appid=" + key1;
            // url to request elevation to geonames.org
            string geonameUrl = "http://api.geonames.org/astergdemJSON?formatted=true&lat=43.70&lng=7.26&username=heleneCollavizza";

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

            string dataGeoname = client.DownloadString(geonameUrl);
            Console.WriteLine("connecting geonames.org");
            // wait a bit for the server to have the time to send its answer
            Thread.Sleep(300);

            // show information of the data (json) that have been sent
            JObject jsonGeoname = JObject.Parse(dataGeoname);
            Console.WriteLine("elevation : " + jsonGeoname.SelectToken("astergdem"));

            bool townExistance = false;
            string dataFromUserTown = null;
            //ask user until he give a valid town
            while (!townExistance)
            {
                //ask user the town 
                Console.WriteLine("please enter a town : ");
                string userTown = Console.ReadLine();
                // new url to ask 
                string urlTownOpenWeather = "http://api.openweathermap.org/data/2.5/weather?q=" + userTown + ",fr&appid=" + key1;
                
                try { 
                    // if an exception is raise the program will ask for a town again
                    dataFromUserTown = client.DownloadString(urlTownOpenWeather);
                    Thread.Sleep(300);
                    townExistance = true;
                } catch (WebException ex )
                {
                    Console.WriteLine( "la ville n'existe pas ou est mal écrite : {0}.", ex.Message);
                    townExistance = false;
                }
                


            }
           
            JObject weatherUserTownJson = JObject.Parse(dataFromUserTown);
            Console.WriteLine("visibility " + weatherUserTownJson.SelectToken("visibility"));
            Console.WriteLine("coord lat " + weatherUserTownJson.SelectToken("coord.lat"));
            Console.WriteLine("coord long " + weatherUserTownJson.SelectToken("coord.lon"));
            Console.WriteLine("weather main " + weatherUserTownJson.SelectToken("weather[0].main"));

            // the end
            Console.WriteLine("press a key to exit");
            Console.ReadKey();
        }
    }
}
