using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HeyMe.UWP
{
    public sealed partial class MainPage
    {
        public static MainPage Instance { get; private set; }
        public MainPage()
        {
            Instance = this;
            this.InitializeComponent();

            LoadApplication(new HeyMe.App());
        }

        public void RunOnUiThread(Action runMe)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                runMe();
            });

        }

    }
}
