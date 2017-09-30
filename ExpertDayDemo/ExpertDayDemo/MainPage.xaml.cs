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
                this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchText.Text);
                this.BindCommand(ViewModel, vm => vm.UpdateCommand, v => v.UpdateBtn);
            });

        }
    }
}
