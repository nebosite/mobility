using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Plugin.Messaging;

namespace HeyMe.Shared
{
    public static class MailHelper
    {
        public static void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            if (!CrossMessaging.IsSupported)
            {
                Debug.WriteLine("Messaging is not supported");
            }

            var emailTask = CrossMessaging.Current.EmailMessenger;
            if (emailTask.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                  .To(addresses)
                  .Subject(subject);
                

                if (body != null) email = email.Body(body);
                if (htmlBody != null) email = email.BodyAsHtml(htmlBody);

                emailTask.SendEmail(email.Build());
            }
            else
            {
                Debug.WriteLine("Can't send an email on this device.");
            }
        }
    }
}
