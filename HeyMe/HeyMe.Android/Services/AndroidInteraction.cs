using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HeyMe.Services;
using HeyMe.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.Droid.Services.AndroidInteraction))]
namespace HeyMe.Droid.Services
{
    public class AndroidInteraction : IDeviceInteraction
    {
        public void MinimizeMe()
        {
            MainActivity.Instance.MinimizeMe();
        }

        public void ShowKeyboard(Guid controlId)
        {
            MainActivity.Instance.ShowKeyboard(controlId);
        }
        public void HideKeyboard(Guid controlId)
        {
            MainActivity.Instance.HideKeyboard(controlId);
        }

        public void RunOnMainThread(Action runMe)
        {
            MainActivity.Instance.RunOnUiThread(runMe);
        }
    }
}