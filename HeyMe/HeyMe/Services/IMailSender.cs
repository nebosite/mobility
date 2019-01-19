using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMe.Services
{
    public interface IMailSender
    {
        void SendMail(string[] addresses, string subject, string body, string htmlBody);
        void PingEmailService();
    }
}
