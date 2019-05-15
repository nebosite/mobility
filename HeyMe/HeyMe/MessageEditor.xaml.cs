using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeyMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageEditor : ContentView
	{
        private AppModel _appModel;

        public MessageEditor ()
		{
			InitializeComponent ();
		}

        public MessageEditor(AppModel appModel)
        {
            _appModel = appModel;
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
    }
}