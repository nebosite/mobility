using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HeyMe.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.iOS.Services.MailSender))]
namespace HeyMe.iOS.Services
{
    public class MailSender : IMailSender
    {
        public void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            throw new NotImplementedException();
        }
    }
}