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

                this.OneWayBind(ViewModel, vm => vm.Temperature, v => v.Temperature.Text);

                this.OneWayBind(ViewModel, vm => vm.IconURL, v => v.WeatherIcon.Source);

            });

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}