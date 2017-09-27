using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickType;
using Refit;

namespace ExpertDayDemo.REST_API
{
    public interface IWeatherAPI
    {
        [Get("/data/2.5/box/city?bbox=6,47,14,54,20&appid=27ac337102cc4931c24ba0b50aca6bbd")]
        IObservable<WeatherInCities> GetWeather();
    }
}
