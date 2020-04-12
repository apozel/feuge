using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private String IdWeather = "327f93f0117f1f0009c3a14ee69ff283";
        private String idGeonames = "heleneCollavizza";
        private WebClient Client = new WebClient();
        private WeatherInfo info;

       
        private void changementInfo()
        {
            resultTextBox.Text = info.ToString();
            sunsetTextBox.Text = info.Sunset.ToString();
        }



        private void startButton_Click(object sender, EventArgs e)
        {
            if (latitudeTextBox.Text.Length != 0 && longitudeTextBox.Text.Length != 0)
            {
                string dataWeatherFromUserTown = null;
                string dataGeonamesFromUserTown = null;
                bool reussite = false;
                try
                {
                    // if an exception is raise the program will ask for a town again
                    dataWeatherFromUserTown = Client.DownloadString(String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", latitudeTextBox.Text, longitudeTextBox.Text, IdWeather));
                    dataGeonamesFromUserTown = Client.DownloadString(String.Format("http://api.geonames.org/timezoneJSON?&lat={0}&lng={1}&username={2}", latitudeTextBox.Text, longitudeTextBox.Text, idGeonames));
                    Thread.Sleep(300);
                    reussite = true;

                }
                catch (WebException ex)
                {
                    MessageBox.Show(string.Format("la ville n'existe pas ou est mal écrite : {0}.", ex.Message));
                }
                if (reussite)
                {
                    WeatherInfo testDayDuration = JsonConvert.DeserializeObject<WeatherInfo>(dataGeonamesFromUserTown);
                    this.info = testDayDuration;
                    changementInfo();
                }

            }
            else if (villeTextBox.Text.Length != 0)
            {

                string urlTownOpenWeather = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0},fr&appid={1}", villeTextBox.Text, IdWeather);
                string rechercheVille = null;
                try
                {
                    // if an exception is raise the program will ask for a town again
                    rechercheVille = Client.DownloadString(urlTownOpenWeather);
                    Thread.Sleep(300);
                    MessageBox.Show(rechercheVille);

                }
                catch (WebException ex)
                {
                    MessageBox.Show(string.Format("la ville n'existe pas ou est mal écrite : {0}.", ex.Message));

                }

                if (rechercheVille.Length != 0)
                {
                    JObject weatherUserTownJson = JObject.Parse(rechercheVille);


                    string dataGeonamesFromUserTown = null;

                    try
                    {
                        // if an exception is raise the program will ask for a town again

                        dataGeonamesFromUserTown = Client.DownloadString(String.Format("http://api.geonames.org/timezoneJSON?&lat={0}&lng={1}&username={2}", double.Parse(weatherUserTownJson.SelectToken("coord.lat").ToString()).ToString(CultureInfo.InvariantCulture), double.Parse(weatherUserTownJson.SelectToken("coord.lon").ToString()).ToString(CultureInfo.InvariantCulture), idGeonames));
                        Thread.Sleep(300);




                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("la ville n'existe pas ou est mal écrite : {0}.", ex.Message);
                    }
                    if (dataGeonamesFromUserTown.Length != 0)
                    {
                        WeatherInfo testDayDuration = JsonConvert.DeserializeObject<WeatherInfo>(dataGeonamesFromUserTown);
                        this.info = testDayDuration;

                        changementInfo();

                    }
                }


            }

        }

       
    }
}
