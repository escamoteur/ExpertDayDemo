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
