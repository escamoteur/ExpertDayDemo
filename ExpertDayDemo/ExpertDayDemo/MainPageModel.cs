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



        }

        public async Task LoadData()
        {
            var restAPI = RestService.For<IWeatherAPI>("http://api.openweathermap.org");

            var result = await restAPI.GetWeather();

            List<CityWeatherItemViewModel> list = new List<CityWeatherItemViewModel>();

            foreach (var city in result.Cities)
            {
                list.Add(new CityWeatherItemViewModel()
                {
                    Name = city.Name,
                    Temperature = city.Main.Temp,
                    IconURL = city.Weather.FirstOrDefault() != null ? "http://openweathermap.org/img/w/" + city.Weather.FirstOrDefault().Icon + ".png": ""
                });
            }

            CityWeatherList = list;


        }
    }
}
