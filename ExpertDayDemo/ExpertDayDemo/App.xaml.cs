using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ExpertDayDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPageModel = new MainPageModel();

            var mainPage = new ExpertDayDemo.MainPage();
            mainPage.BindingContext = mainPageModel;

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
