// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = WeatherInCities.FromJson(jsonString);
//
// For POCOs visit quicktype.io?poco
//
namespace QuickType
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public  class WeatherInCities
    {
        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("calctime")]
        public double Calctime { get; set; }

        [JsonProperty("cod")]
        public long Cod { get; set; }

        [JsonProperty("list")]
        public List<City> Cities { get; set; }
    }

    public  class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("snow")]
        public object Snow { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }

    public  class Coord
    {
        [JsonProperty("Lat")]
        public double Lat { get; set; }

        [JsonProperty("Lon")]
        public double Lon { get; set; }
    }

    public  class Clouds
    {
        [JsonProperty("today")]
        public long Today { get; set; }
    }

    public  class Main
    {
        [JsonProperty("sea_level")]
        public double? SeaLevel { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("grnd_level")]
        public double? GrndLevel { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
    }

    public  class Rain
    {
        [JsonProperty("3h")]
        public double The3h { get; set; }
    }

    public  class Weather
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }
    }

    public  class Wind
    {
        [JsonProperty("deg")]
        public double Deg { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }
    }

}
