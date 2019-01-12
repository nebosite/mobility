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
    //------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    //------------------------------------------------------------------------------
    public class UWPInteraction : IDeviceInteraction
    {
        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public void MinimizeMe()
        {
            Debug.WriteLine("Can't minimize a uwp app");
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public void ShowKeyboard(Guid controlId)
        {

        }
        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public void HideKeyboard(Guid controlId)
        {
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public void RunOnMainThread(Action runMe)
        {
            MainPage.Instance.RunOnUiThread(runMe);
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        public bool StartVoiceRecognition()
        {
            return false;
        }
    }
}
