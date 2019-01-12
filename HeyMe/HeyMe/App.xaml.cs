using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HeyMe
{
    public partial class App : Application
    {
        AppModel _theModel;

        //------------------------------------------------------------------------------
        /// <summary>
        /// ctor
        /// </summary>
        //------------------------------------------------------------------------------
        public App()
        {
            InitializeComponent();

            _theModel = new AppModel();
            MainPage = new MainPage(_theModel);

            this.BindingContext = _theModel;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        //------------------------------------------------------------------------------
        protected override void OnResume()
        {
            ((MainPage)MainPage).Resume();
        }
    }
}
