using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ExpertDayDemo
{
    public class CityWeatherItemViewModel
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string Icon { get; set ; }
    }
}
