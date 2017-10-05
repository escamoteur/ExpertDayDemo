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
using Refit;

namespace ExpertDayDemo
{
    public class MainPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                    IconURL = city.Weather.FirstOrDefault() != null ? "http://openweathermap.org/img/w/" + city.Weather.FirstOrDefault().Icon + ".png" : ""

                })
                .ToList()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(list =>
                {
                    CityWeatherList = list;
                });
        }
    }
}

