using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.UWP.Services.UWPInteraction))]
namespace HeyMe.UWP.Services
{
    public class UWPInteraction : IDeviceInteraction
    {
        public void MinimizeMe()
        {
            Debug.WriteLine("Can't minimize a uwp app");
        }

        public void ShowKeyboard(Guid controlId)
        {

        }
        public void HideKeyboard(Guid controlId)
        {
        }

        public void RunOnMainThread(Action runMe)
        {
            MainPage.Instance.RunOnUiThread(runMe);
        }
    }
}
