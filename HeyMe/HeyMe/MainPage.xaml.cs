using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HeyMe
{
    //------------------------------------------------------------------------------
    /// <summary>
    /// MainPage
    /// </summary>
    //------------------------------------------------------------------------------
    public partial class MainPage : ContentPage
    {
        AppModel _appModel;
        IDeviceInteraction _interactor;

        //------------------------------------------------------------------------------
        /// <summary>
        /// ctor
        /// </summary>
        //------------------------------------------------------------------------------
        public MainPage()
        {
            InitializeComponent();
            _appModel = App.TheModel;

            var alertView = new AlertView(_appModel);
            var messageEditor = new MessageEditor(_appModel);

        }

        Task _appearingTask;

        //------------------------------------------------------------------------------
        /// <summary>
        /// OnAppearing
        /// </summary>
        //------------------------------------------------------------------------------
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _appearingTask = Task.Run(async () =>
            {
                await Task.Delay(100);
                _appModel.Start();
                if (!_interactor.StartVoiceRecognition())
                {
                    //EmailBodyEditor.Focus();
                    _interactor.ShowKeyboard(EmailBodyEditor.Id);
                }
            });
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Resume
        /// </summary>
        //------------------------------------------------------------------------------
        internal void Resume()
        {
            _appModel.Start();
            if(!_interactor.StartVoiceRecognition())
            {
                _interactor.ShowKeyboard(EmailBodyEditor.Id);
            }
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// SendButtonClicked
        /// </summary>
        //------------------------------------------------------------------------------
        private void SendButtonClicked(object sender, EventArgs e)
        {
            _appModel.Send();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// SendButtonClicked
        /// </summary>
        //------------------------------------------------------------------------------
        private void CancelButtonClicked(object sender, EventArgs e)
        {
            _appModel.Cancel();
            _interactor.MinimizeMe();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        private void AlertOKClicked(object sender, EventArgs e)
        {
            _appModel.SendError = null;
            _interactor.MinimizeMe();
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// UtilityButtonClicked
        /// </summary>
        //------------------------------------------------------------------------------
        private void UtilityButtonClicked(object sender, EventArgs e)
        {
         

        }
    }
}
