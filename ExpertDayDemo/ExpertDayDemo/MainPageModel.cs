using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
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



        public ReactiveCommand<Unit,Unit>   UpdateCommand { get; set; }

        public MainPageModel()
        {
            UpdateWeather();

            UpdateCommand = ReactiveCommand.Create(() => UpdateWeather());

            UpdateCommand.IsExecuting
                .Subscribe(busy => Debug.WriteLine("Update running: " + busy.ToString()));

        }

        public void UpdateWeather()
        {
            // We are using Refit to make the REST call
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

