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
            _appModel.OnSendComplete += () =>
            {
                _interactor.HideKeyboard(EmailBodyEditor.Id);
                _interactor.MinimizeMe();
            };

            _interactor = DependencyService.Get<IDeviceInteraction>();

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                _appModel.RecieveSpeechText(args);
                EmailBodyEditor.Focus();
                _interactor.ShowKeyboard(EmailBodyEditor.Id);
            });

            MessagingCenter.Subscribe<IMessageSender, TouchInfo>(this, "Touched", (sender, touchInfo) =>
            {
                _appModel.RecievedTouch(touchInfo);
            });

            var timerTapRecognizer = new TapGestureRecognizer();
            timerTapRecognizer.Tapped += (s, e) => {
                _appModel.TimerTapped();
            };

            InputProgress.GestureRecognizers.Add(timerTapRecognizer);
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
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Xamarin.Forms.Forms.Init(e);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

        }
    }
}
