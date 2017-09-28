using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpertDayDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeatherItemView 
    {
        public CityWeatherItemView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Name, v => v.CityName.Text);

                this.OneWayBind(ViewModel, vm => vm.Temperature, v => v.Temperature.Text, value => Convert.ToString(value,new CultureInfo("de-DE")) + "°" );

                this.OneWayBind(ViewModel, vm => vm.Icon, v => v.WeatherIcon.Source,iconID => "http://openweathermap.org/img/w/" + iconID + ".png");

            });

        }






    }
}