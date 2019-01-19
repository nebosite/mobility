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
        /// <summary>
        /// Event to notify send completion
        /// </summary>
        public event Action OnSendComplete;

        /// <summary>
        /// Emails we can send to
        /// </summary>
        public string[] EmailChoices { get; private set; }

        /// <summary>
        /// Selected Email Address
        /// </summary>
        private string _selectedEmail;
        public string SelectedEmail
        {
            get => _selectedEmail;
            set
            {
                _selectedEmail = value;
                RaisePropertyChanged(nameof(SelectedEmail));
                NonInputTime = 0;
            }
        }

        /// <summary>
        /// The current ticks that have passed by without input
        /// </summary>
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

        /// <summary>
        /// Max number of ticks to wait without input before sending the current message
        /// </summary>
        public int NonInputLimit { get; set; } = 40;

        /// <summary>
        /// Conversion of noninput time into a 0.0 - 1.0 progress value
        /// </summary>
        public double ProgressValue =>(double) NonInputTime / NonInputLimit;

        /// <summary>
        /// Email text
        /// </summary>
        string _emailText;
        public string EmailText
        {
            get => _emailText;
            set
            {
                _emailText = value;
                NonInputTime = 0;
                RaisePropertyChanged(nameof(EmailText));
            }
        }

        private IMailSender _mailSender;
        private IDeviceInteraction _interactor;
        private Timer _inputTimer;
        private int _timerIncrement = 1;

        //------------------------------------------------------------------------------
        /// <summary>
        /// ctor
        /// </summary>
        //------------------------------------------------------------------------------
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

        //------------------------------------------------------------------------------
        /// <summary>
        /// Start a fresh email entry
        /// </summary>
        //------------------------------------------------------------------------------
        internal void Start()
        {
            _mailSender.PingEmailService();
            EmailText = "";
            _timerIncrement = 1;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Called by the system when a touch was detected
        /// </summary>
        //------------------------------------------------------------------------------
        internal void RecievedTouch(TouchInfo touchInfo)
        {
            NonInputTime = 0;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// pause or unpause the timer
        /// </summary>
        //------------------------------------------------------------------------------
        internal void TimerTapped()
        {
            _timerIncrement = (_timerIncrement == 0 ? 1 : 0);
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Called by the system when a touch was detected
        /// </summary>
        //------------------------------------------------------------------------------
        internal void Cancel()
        {
            EmailText = "";
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Add voice recognition text to this email
        /// </summary>
        //------------------------------------------------------------------------------
        internal void RecieveSpeechText(string args)
        {
            EmailText += args + "\r\n";
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Input timer handler
        /// </summary>
        //------------------------------------------------------------------------------
        private void _inputTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailText))
            {
                NonInputTime += _timerIncrement;
                if (NonInputTime == NonInputLimit) Send();
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        //------------------------------------------------------------------------------
        /// <summary>
        /// Called by the system when a touch was detected
        /// </summary>
        //------------------------------------------------------------------------------

        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        //------------------------------------------------------------------------------
        /// <summary>
        /// Send the message we have queued up
        /// </summary>
        //------------------------------------------------------------------------------
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
