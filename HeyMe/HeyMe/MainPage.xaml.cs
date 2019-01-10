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
        public MainPage(AppModel model)
        {
            InitializeComponent();
            _appModel = model;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var interactor = DependencyService.Get<IDeviceInteraction>();
            Task.Run(async () =>
            {
                await Task.Delay(100);
                EmailBodyEditor.Focus();
                interactor.ShowKeyboard(EmailBodyEditor.Id);
            });
        }

        IDeviceInteraction _mailSender;
        private void SendButtonClicked(object sender, EventArgs e)
        {
            if(_mailSender == null)
            {

            }
            _appModel.Send();
            var interactor = DependencyService.Get<IDeviceInteraction>();

            interactor.HideKeyboard(EmailBodyEditor.Id);
            interactor.MinimizeMe();
        }
    }
}
