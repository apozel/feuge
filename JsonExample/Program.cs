﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExample
{
    class Program
    {
        // this class shows examples for parsing json files
        // many web services send json data in response to a request form a client 
        /*  https://www.w3schools.com/js/js_json_intro.asp
         * JSON: JavaScript Object Notation.
         * JSON is a syntax for storing and exchanging data.
         * JSON is text, written with JavaScript object notation.*/

        // WARNING : to use this code you need to install NewtonSoft.Json package. 
        //           projet -> gérer les packages nuget -> rechercher NewtonSoft.Json

        static void Main(string[] args)
        {

            // First way to use json information : create a JObject and access the fields
            /////////////////////////////////////

            // this is an example of json data from a geography web service
            // @ is to convert string to utf-8 format
            string timeZoneJson = @"{ 'sunrise':'2018-04-04 06:55','sunset':'2018-04-04 19:52','countryName':'Austria','gmtOffset':1,'timezoneId':'Europe/Vienna'}";

            JObject jsot = JObject.Parse(timeZoneJson);
            Console.WriteLine("select token sunrise : " + jsot.SelectToken("sunrise"));
            Console.WriteLine("select token timezoneid : " + jsot.SelectToken("timezoneId"));
            Console.WriteLine("select token contryName : " + jsot.SelectToken("countryName"));


            // a more complicated example taken from another request
            // it contains arrays (i.e. []), and sub-tokens (i.e. {} inside {})
            string weatherJson = @"{ 'coord':{ 'lon':-0.13,'lat':51.51},'weather':[{'id':300,'main':'Drizzle','description':'light intensity drizzle','icon':'09d'}],'base':'stations','main':{'temp':280.32,'pressure':1012,'humidity':81,'temp_min':279.15,'temp_max':281.15},'visibility':10000,'wind':{'speed':4.1,'deg':80},'clouds':{'all':90},'dt':1485789600,'sys':{'type':1,'id':5091,'message':0.0103,'country':'GB','sunrise':1485762037,'sunset':1485794875},'id':2643743,'name':'London','cod':200}";

            JObject jsow = JObject.Parse(weatherJson);
            // simple token
            Console.WriteLine("visibility " + jsow.SelectToken("visibility"));
            // token with a sub-token
            Console.WriteLine("coord lat " + jsow.SelectToken("coord.lat"));
            Console.WriteLine("coord long " + jsow.SelectToken("coord.lon"));
            // token with an array
            Console.WriteLine("weather main " + jsow.SelectToken("weather[0].main"));


            // Second way to use json information : create your own object with the same information
            // can be useful if you need to implement a sophisticated behavior
            /////////////////////////////////////
            // this converts a json object into a WeatherInfo object
            WeatherInfo i = JsonConvert.DeserializeObject<WeatherInfo>(timeZoneJson);
            Console.WriteLine("sunrise using my own object " + (i.Sunrise));
            Console.WriteLine("day duration " + i.DayDuration() + " hours");
            i.PrintJetLag();

            string testDayDurationJson = @"{ 'sunrise':'2018-04-04 06:55','sunset':'2018-04-04 21:52','countryName':'Mongolia','gmtOffset':8,'timezoneId':'Europe/Vienna'}";

            
            WeatherInfo testDayDuration = JsonConvert.DeserializeObject<WeatherInfo>(testDayDurationJson);
            Console.WriteLine("sunrise using my own object " + (testDayDuration.Sunrise));
            Console.WriteLine("day duration " + testDayDuration.DayDuration() + " hours");
            testDayDuration.PrintJetLag();

            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }
    }
}

