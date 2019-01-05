﻿using HeyMe.Services;
using System.Diagnostics;
using Plugin.Messaging;
using HeyMe.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(HeyMe.UWP.Services.MailSender))]
namespace HeyMe.UWP.Services
{
    public class MailSender : IMailSender
    {
        public void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            MailHelper.SendMail(addresses, subject, body, htmlBody);
        }
    }
}
