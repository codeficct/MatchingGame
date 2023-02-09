using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingGame.Droid
{
    [Activity(Label = "Matching Game", MainLauncher = false, Theme = "@style/MyTheme.Splash",
        NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize )]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }

        private async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}