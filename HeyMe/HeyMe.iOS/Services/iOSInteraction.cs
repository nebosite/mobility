using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HeyMe.Services;
using UIKit;
using Windows.UI.Core;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.iOS.Services.iOSInteraction))]
namespace HeyMe.iOS.Services
{
    public class iOSInteraction : IDeviceInteraction
    {
        public void MinimizeMe()
        {
            throw new NotImplementedException();
        }

        public void ShowKeyboard(Guid controlId)
        {
            throw new NotImplementedException();
        }
        public void HideKeyboard(Guid controlId)
        {
            throw new NotImplementedException();
        }

        public void RunOnMainThread(Action runMe)
        {
            throw new NotImplementedException();

            //UIApplication.SharedApplication.InvokeOnMainThread(delegate
            //{
            //    // Code to run here
            //});
        }

        public bool StartVoiceRecognition()
        {
            return false;
        }
    }
}