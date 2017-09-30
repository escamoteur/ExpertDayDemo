using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExpertDayDemo.REST_API;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Refit;

namespace ExpertDayDemo
{
    public class MainPageModel : ReactiveObject
    {
        [Reactive]
        public IEnumerable<CityWeatherItemViewModel> CityWeatherList { get; set; }


        public MainPageModel()
        {
            UpdateWeather();
        }

        public void UpdateWeather()
        {
            var restAPI = RestService.For<IWeatherAPI>("http://api.openweathermap.org");

            restAPI.GetWeather()
                .SelectMany(result => result.Cities)
                .Select(city => new CityWeatherItemViewModel()
                {
                    Name = city.Name,
                    Temperature = city.Main.Temp,
                    Icon = city.Weather.FirstOrDefault() != null
                        ?  city.Weather.FirstOrDefault().Icon
                        : ""
                })
                .ToList()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(list => CityWeatherList = list);
        }
    }
}

