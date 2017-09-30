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
        public IObservable<bool> CanFilter { get; set; }

        [Reactive]
        public IEnumerable<CityWeatherItemViewModel> CityWeatherList { get; set; }

        public IEnumerable<CityWeatherItemViewModel> LastUpdatedList { get; set; }

        [Reactive]
        public string SearchText { get; set; }

        public ReactiveCommand<Unit, IList<CityWeatherItemViewModel>>   UpdateCommand { get; set; }
        public ReactiveCommand<string, Unit> FilterCommand { get; set; }

        public ReactiveCommand<Unit, Unit> DoSomeImportantOtherTaskCommand { get; set; }




        public MainPageModel()
        {
            UpdateWeather()
                .Subscribe(list =>
                {
                    LastUpdatedList = list;
                    CityWeatherList = list;
                });

            // Add some pseudo time based event    
            var TickEvent = Observable.Interval(TimeSpan.FromMilliseconds(5000));
                

            TickEvent
                .Subscribe(_ => Debug.WriteLine("Timer Event"));


            // Create Commands -----------------------------------------------

            UpdateCommand = ReactiveCommand.CreateFromObservable(() => UpdateWeather());
            UpdateCommand
                .Subscribe(list => CityWeatherList = list);

            UpdateCommand.ThrownExceptions.Subscribe(exception => Debug.WriteLine(exception.Message));


            DoSomeImportantOtherTaskCommand = ReactiveCommand.CreateFromTask(async () => await DoSomethingImportant());
            DoSomeImportantOtherTaskCommand.ThrownExceptions.Subscribe(exception => Debug.WriteLine(exception.Message));

            TickEvent
                .Subscribe(_ => DoSomeImportantOtherTaskCommand.Execute().Subscribe());





            //we want to prevent that any command is executed while one of the others is running
            CanFilter = DoSomeImportantOtherTaskCommand.IsExecuting
                .Merge(UpdateCommand.IsExecuting)
                    .DistinctUntilChanged()
                        .Select(b => !b);

            CanFilter.Subscribe(b => Debug.WriteLine("CanFilter: " + b));
    
            FilterCommand = ReactiveCommand.CreateFromTask<string>(async name => await FilterCities(name), CanFilter);





            // Filter changes ------------------------------------------------------------------------------
            IObservable<string> TextChanging = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500));
               
                    

            TextChanging
                .InvokeCommand(FilterCommand);


        }

        private async Task DoSomethingImportant()
        {
            await Task.Delay(300);

        }

        private async Task FilterCities(string filter)
        {
            if (CityWeatherList != null)
            {
                CityWeatherList = LastUpdatedList.Where(city => string.IsNullOrWhiteSpace(filter) || city.Name.ToUpper().Contains(filter.ToUpper()));
            }
        }

        public IObservable<IList<CityWeatherItemViewModel>> UpdateWeather()
        {
            // We are using Refit to make the REST call
            var restAPI = RestService.For<IWeatherAPI>("http://api.openweathermap.org");

            SearchText = "";

            return restAPI.GetWeather()
                .Delay(TimeSpan.FromSeconds(1)) // Just to make it a bit slower so that we can see the disabling of the control
               .SelectMany(result => result.Cities)
                .Select(city => new CityWeatherItemViewModel()
                {
                    Name = city.Name,
                    Temperature = city.Main.Temp,
                    Icon = city.Weather.FirstOrDefault() != null ? city.Weather.FirstOrDefault().Icon
                        : ""
                })
                .ToList()
                .ObserveOn(RxApp.MainThreadScheduler);

        }
    }
}

