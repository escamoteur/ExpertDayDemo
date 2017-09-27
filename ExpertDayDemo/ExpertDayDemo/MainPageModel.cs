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

            var restAPI = RestService.For<IWeatherAPI>("http://api.openweathermap.org");

            restAPI.GetWeather()
                .SelectMany(cities => cities.List)
                    .Buffer(2)
                    .Select(cities => new CityWeatherItemViewModel()
                                            {
                                                Name = cities[0].Name,
                                                Temperature = cities[0].Main.Temp,
                                                IconURL = cities[0].Weather.FirstOrDefault() !=  null ? "http://openweathermap.org/img/w/" + cities[0].Weather.FirstOrDefault().Icon + ".png" : "",

                                                Name2 = cities.Count > 1 ? cities[1].Name : "",
                                                Temperature2 = cities.Count > 1 ? cities[1].Main.Temp : 0,
                                                IconURL2 = cities.Count > 1 && cities[1].Weather.FirstOrDefault() !=  null ? "http://openweathermap.org/img/w/" + cities[1].Weather.FirstOrDefault().Icon + ".png" : ""
                                            })
                        .ToList()
                            .ObserveOn(RxApp.MainThreadScheduler)
                                .Subscribe(list => CityWeatherList = list);


        }
    }
}
