using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using Xamarin.Forms;

namespace ExpertDayDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var broker = Locator.Current.GetService<IMessageBroker>();

            broker.QueueMessage("From PCL Project");
        }
    }
}
