using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HeyMe
{
    public partial class App : Application
    {
        AppModel _theModel;
        public App()
        {
            InitializeComponent();

            _theModel = new AppModel();
            MainPage = new MainPage(_theModel);

            this.BindingContext = _theModel;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
