using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpertDayDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeatherItemView :IViewFor<CityWeatherItemViewModel>
    {
        public CityWeatherItemView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
            });

        }





        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.ViewModel = this.BindingContext as CityWeatherItemViewModel;
        }



        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (CityWeatherItemViewModel) value; }
        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(
            nameof(ViewModel),
            typeof(CityWeatherItemViewModel),
            typeof(CityWeatherItemView),
            default(CityWeatherItemViewModel),
            BindingMode.OneWay,
            propertyChanged: OnViewModelChanged);

        private static void OnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            bindableObject.BindingContext = newValue;
        }

        public CityWeatherItemViewModel ViewModel { get; set; }
    }
}