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
using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using View = Android.Views.View;

namespace HeyMe.Droid
{
    [Activity(Label = "HeyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }
        bool _created = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);
            _created = true;
            Trace("Created");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        static void Trace(string message)
        {
            Android.Util.Log.Info("EricTrace", message);
        }

        Dictionary<Guid, View> _registeredViews = new Dictionary<Guid, View>();

        public void RegisterView(Guid controlId, View view)
        {
            _registeredViews[controlId] = view;
        }

        public void ShowKeyboard(Guid controlId)
        {
            if(!_created) return;
            
            var inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            if (_registeredViews.TryGetValue(controlId, out var targetView))
            {
                inputManager.ShowSoftInput(targetView, ShowFlags.Forced);
            }
        }
        public void HideKeyboard(Guid controlId)
        {
            if (!_created) return;

            var inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            if (_registeredViews.TryGetValue(controlId, out var targetView))
            {
                inputManager.ShowSoftInput(targetView, ShowFlags.Forced);
                inputManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.None);
            }
        }


        public void MinimizeMe()
        {
            Task.Run(async () =>
            {
                await Task.Delay(100);
                var main = new Intent(Intent.ActionMain);
                main.AddCategory(Intent.CategoryHome);
                main.SetFlags(ActivityFlags.NewTask);
                StartActivity(main);
            });
        }
    }
}