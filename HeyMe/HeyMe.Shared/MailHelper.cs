using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Messaging;

namespace HeyMe.Shared
{
    //------------------------------------------------------------------------------
    /// <summary>
    /// COmmon code for sending SMTP mail through a special service
    /// </summary>
    //------------------------------------------------------------------------------
    public static class MailHelper
    {
        static HttpClient _client = new HttpClient();

        //------------------------------------------------------------------------------
        /// <summary>
        /// Message contents
        /// </summary>
        //------------------------------------------------------------------------------
        public class EmailMessage
        {
            public string To { get; set; }
            public string From { get; set; }
            public string FromName { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string HtmlBody { get; set; }
        }

        const string _sendMailApi = "https://niftiprotoservices.azurewebsites.net/api/SendMail?code=jFtAugXVQdKYa3vEwNMUkvCz/4ZWlIowIqplUIfiJ1KJ700bBcORAg==";
      //const string _sendMailApi = "https://niftiprotoservices.azurewebsites.net/api/SendMail?code=jFtAugXVQdKYa3vEwNMUkvCz/4ZWlIowIqplUIfiJ1KJ700bBcORAg==";

        //------------------------------------------------------------------------------
        /// <summary>
        /// SendMail through a private service
        /// </summary>
        //------------------------------------------------------------------------------
        public static void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            foreach(var address in addresses)
            {
                Debug.WriteLine($"Sending to {address}");
                var message = new EmailMessage
                {
                    To = address,
                    From = "heyme@noreply.org",
                    FromName = "HEY ME",
                    Subject = subject,
                    Body = (body == null ? null : body + "\r\n\r\nSent from Hey Me"),
                    HtmlBody = (htmlBody == null ? null : htmlBody + "<p>Sent from Hey Me</p>")
                };

                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                try
                {
                    var response = _client.PostAsync(_sendMailApi, content).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        Debug.WriteLine("Web request error: " + responseString);
                    }
                }
                catch(AggregateException ae)
                {
                    foreach(var err in ae.InnerExceptions )
                    {
                        Debug.WriteLine("ARRG: " + err.Message);
                    }
                    throw ae.InnerException;
                }
                Debug.WriteLine($"Finished sending to {address}");
            }
        }

        static Task _pingTask;
        //------------------------------------------------------------------------------
        /// <summary>
        /// Wake up the mail service so it is primed to respond quickly
        /// </summary>
        //------------------------------------------------------------------------------
        public static void PingMailService()
        {
            Debug.WriteLine("StartPing");
            _pingTask = Task.Run(async () =>
            {
                try
                {
                    var message = new EmailMessage { To = "ping"  };
                    var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                    await _client.PostAsync(_sendMailApi, content);
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Ping Error: " + e.Message);
                }
                Debug.WriteLine("PingFinished");
            });
        }

    }
}
