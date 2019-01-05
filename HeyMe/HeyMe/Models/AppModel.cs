using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace HeyMe
{
    public class AppModel: INotifyPropertyChanged
    {
        public string[] EmailChoices { get; private set; }

        public string SelectedEmail { get; set; }
        public string EmailText { get; set; }

        public AppModel()
        {
            EmailChoices = new string[]
            {
                "ericjorg@thejcrew.net",
                "evarmint22@gmail.com"
            };

            SelectedEmail = EmailChoices[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void Send()
        {
            var paragraphSpot = EmailText.IndexOf("paragraph");
            if(paragraphSpot > 0)
            {
                EmailText = EmailText.Replace("paragraph", "\n");
            }
            var parts = EmailText.Split(new char[] { '\n' }, 2);
            var subject = "An email note";
            var body = EmailText;
            if(parts.Length > 1)
            {
                subject = parts[0];
                body = parts[1];
            }
            DependencyService.Get<IMailSender>().SendMail(SelectedEmail.Split(';'), subject, body, null);
            EmailText = "";
            RaisePropertyChanged(nameof(EmailText));
        }
    }
}
