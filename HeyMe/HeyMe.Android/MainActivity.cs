using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HeyMe.Services;
using Android.Content;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.Droid.MainActivity))]
namespace HeyMe.Droid
{
    [Activity(Label = "HeyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,  IDeviceInteraction
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public void MinimizedMe()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000);

                var main = new Intent(Intent.ActionMain);
                main.AddCategory(Intent.CategoryHome);
                main.SetFlags(ActivityFlags.NewTask);
                Instance.StartActivity(main);

            });
        }
    }
}