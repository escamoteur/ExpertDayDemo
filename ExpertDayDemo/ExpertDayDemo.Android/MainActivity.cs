using System;

using Android.App;
using Android.Content.PM;

using Android.OS;
using Splat;

namespace ExpertDayDemo.Droid
{
    [Activity(Label = "ExpertDayDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;





            base.OnCreate(bundle);

            var messageBroker = new MessageBroker();

            // Register a real instance of MessageBroker
            Locator.CurrentMutable.RegisterConstant<IMessageBroker>(messageBroker);


            messageBroker.Messages.Subscribe(s => System.Diagnostics.Debug.WriteLine(s));


            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

