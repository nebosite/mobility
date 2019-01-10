using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace HeyMe
{
    public class AppModel: INotifyPropertyChanged
    {
        public event Action OnSendComplete;

        public string[] EmailChoices { get; private set; }

        public string SelectedEmail { get; set; }

        private int _nonInputTime = 0;
        public int NonInputTime
        {
            get => _nonInputTime;
            set
            {
                _nonInputTime = value;
                RaisePropertyChanged(nameof(NonInputTime));
                RaisePropertyChanged(nameof(ProgressValue));
            }
        }

        public int NonInputLimit { get; set; } = 50;

        public double ProgressValue =>(double) NonInputTime / NonInputLimit;

        string _emailText;

        private IMailSender _mailSender;
        private IDeviceInteraction _interactor;

        public string EmailText
        {
            get => _emailText;
            set
            {
                _emailText = value;
                Debug.WriteLine("Text changed");
                NonInputTime = 0;
                RaisePropertyChanged(nameof(EmailText));
            }
        }

        Timer _inputTimer;

        public AppModel()
        {
            EmailChoices = new string[]
            {
                "ericjorg@thejcrew.net",
                "evarmint22@gmail.com",
                "evarmint22@gmail.com;eric@thejcrew.net"
            };

            _mailSender = DependencyService.Get<IMailSender>();
            _interactor = DependencyService.Get<IDeviceInteraction>();

            SelectedEmail = EmailChoices[0];
            _inputTimer = new Timer(100);
            _inputTimer.Elapsed += _inputTimer_Elapsed;

            _inputTimer.Start();
        }

        private void _inputTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailText))
            {
                NonInputTime++;
                if (NonInputTime == NonInputLimit) Send();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        internal void Send()
        {
            var sendText = EmailText;
            var paragraphSpot = sendText.IndexOf("paragraph");
            if(paragraphSpot > 0)
            {
                sendText = sendText.Replace("paragraph", "\n");
            }
            var parts = sendText.Split(new char[] { '\n', '\r' }, 2);
            var body = "";
            var subject = parts[0];
            if(parts[0].Length > 100)
            {
                subject = parts[0].Substring(0, 100);
                body = parts[0] + "\r\n";
            }
            if(parts.Length > 1)
            {
                body += parts[1];
            }
            _mailSender.SendMail(SelectedEmail.Split(';'), subject, body, null);

            _interactor.RunOnMainThread(() =>
            {
                EmailText = "";
                RaisePropertyChanged(nameof(EmailText));
                OnSendComplete?.Invoke();
            });
           
        }
    }
}
