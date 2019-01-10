using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Plugin.Messaging;

namespace HeyMe.Shared
{
    public static class MailHelper
    {
        static HttpClient _client = new HttpClient();

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

        public static void SendMail(string[] addresses, string subject, string body, string htmlBody)
        {
            foreach(var address in addresses)
            {
                var message = new EmailMessage
                {
                    To = address,
                    From = "heyme@noreply.org",
                    FromName = "HEY ME",
                    Subject = subject,
                    Body = (body == null ? "" : body) + "\r\n\r\nSent from Hey Me",
                    HtmlBody = (htmlBody == null ? "" : htmlBody) + "<p>Sent from Hey Me</p>"
                };

                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(_sendMailApi, content).Result;

                if(!response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("Web request error: " + responseString);
                }
            }
        }
    }
}
