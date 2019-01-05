using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.UWP.Services.UWPInteraction))]
namespace HeyMe.UWP.Services
{
    public class UWPInteraction : IDeviceInteraction
    {
        public void MinimizedMe()
        {
            Debug.WriteLine("Can't minimize a uwp app");
        }
    }
}
