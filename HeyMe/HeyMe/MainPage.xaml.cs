using HeyMe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HeyMe
{
    public partial class MainPage : ContentPage
    {
        AppModel _appModel;
        IDeviceInteraction _interactor;
        public MainPage(AppModel model)
        {
            InitializeComponent();
            _appModel = model;
            _appModel.OnSendComplete += () =>
            {
                _interactor.HideKeyboard(EmailBodyEditor.Id);
                _interactor.MinimizeMe();
            };

            _interactor = DependencyService.Get<IDeviceInteraction>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await Task.Delay(100);
                EmailBodyEditor.Focus();
                _interactor.ShowKeyboard(EmailBodyEditor.Id);
            });
        }

        internal void Resume()
        {
            _interactor.ShowKeyboard(EmailBodyEditor.Id);
        }

        private void SendButtonClicked(object sender, EventArgs e)
        {
            _appModel.Send();
        }
    }
}
