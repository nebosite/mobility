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

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.Droid.Services.AndroidMailSender))]
namespace HeyMe.Droid.Services
{
    public class AndroidMailSender : IMailSender
    {
        public void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            MailHelper.SendMail(addresses, subject, body, htmlBody);
        }

    }
}