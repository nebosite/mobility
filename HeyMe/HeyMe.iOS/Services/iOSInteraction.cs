using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HeyMe.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.iOS.Services.iOSInteraction))]
namespace HeyMe.iOS.Services
{
    public class iOSInteraction : IDeviceInteraction
    {
        public void MinimizedMe()
        {
            throw new NotImplementedException();
        }
    }
}