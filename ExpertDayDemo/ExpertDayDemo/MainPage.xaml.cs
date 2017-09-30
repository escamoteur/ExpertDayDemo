using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExpertDayDemo
{
 
    public partial class MainPage 
    {
        public MainPage()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.SearchText, v => v.FilterText.Text);
                this.BindCommand(ViewModel, vm => vm.UpdateCommand, v => v.UpdateBtn);


                // Normally we would just bind to the IsEnabled property. Unfortunately the color of a disabled Entry is 
                // only restored when getting focus again. So to get a better visual for the demo we change the background color
                this.WhenAnyObservable(x => x.ViewModel.CanFilter)
                    .Subscribe(active =>
                    {
                        if (active)
                        {
                            FilterText.BackgroundColor = Color.White;

                        }
                        else
                        {
                            FilterText.BackgroundColor = Color.DarkGray;
                        }
                    });
            });

        }
    }
}
