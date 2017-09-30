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

        [Reactive]
        public string SearchText { get; set; }


        public ReactiveCommand<Unit,Unit>   UpdateCommand { get; set; }
        public ReactiveCommand<string, Unit> FilterCommand { get; set; }



        public MainPageModel()
        {
            UpdateWeather();


            FilterCommand = ReactiveCommand.CreateFromTask<string>(async name => await FilterCities(name));


            UpdateCommand = ReactiveCommand.Create(() => UpdateWeather(SearchText),
                FilterCommand.IsExecuting.Select(b => !b));


            // Filter changes
            IObservable<string> TextChanging = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500));
                    

            TextChanging
                .InvokeCommand(FilterCommand);





            UpdateCommand.IsExecuting
                .Subscribe(busy => Debug.WriteLine("Update running: " + busy.ToString()));


        }

        private async Task FilterCities(string filter)
        {
            await Task.Delay(1000);
            CityWeatherList = CityWeatherList.Where(city => string.IsNullOrWhiteSpace(filter) || city.Name.ToUpper().Contains(filter.ToUpper()));

        }

        public void UpdateWeather(string filter = "")
        {
            // We are using Refit to make the REST call
            var restAPI = RestService.For<IWeatherAPI>("http://api.openweathermap.org");

            restAPI.GetWeather()
                .SelectMany(result => result.Cities)
                    .Where(city => string.IsNullOrWhiteSpace(filter) || city.Name.ToUpper().Contains(filter.ToUpper()) )
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

