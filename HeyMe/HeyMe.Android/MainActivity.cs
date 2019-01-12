using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HeyMe.Services;
using Android.Content;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using View = Android.Views.View;
using Android.Speech;

namespace HeyMe.Droid
{
    [Activity(Label = "HeyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IMessageSender
    {
        public static MainActivity Instance { get; private set; }
        bool _created = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);
            _created = true;
            Trace("Created");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            MessagingCenter.Send<IMessageSender, TouchInfo>(this, "Touched", new TouchInfo());
            return base.DispatchTouchEvent(ev);
        }

        const int VOICE_ACTIVITY = 235;
        internal bool StartVoiceRecognition()
        {
            string rec = global::Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec == "android.hardware.microphone")
            {
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak!!");
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                StartActivityForResult(voiceIntent, VOICE_ACTIVITY);
                return true;
            }

            return false;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == VOICE_ACTIVITY)
            {
                if (resultCode == Result.Ok)
                {
                    var partials = data.GetStringArrayListExtra(RecognizerIntent.ExtraPartialResults);

                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        MessagingCenter.Send<IMessageSender, string>(this, "STT", matches[0]);
                    }
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

        static void Trace(string message)
        {
            Android.Util.Log.Info("EricTrace", message);
        }

        Dictionary<Guid, View> _registeredViews = new Dictionary<Guid, View>();

        public void RegisterView(Guid controlId, View view)
        {
            _registeredViews[controlId] = view;
        }

        public void ShowKeyboard(Guid controlId)
        {
            if(!_created) return;
            
            var inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            if (_registeredViews.TryGetValue(controlId, out var targetView))
            {
                inputManager.ShowSoftInput(targetView, ShowFlags.Forced);
            }
        }
        public void HideKeyboard(Guid controlId)
        {
            if (!_created) return;

            var inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            if (_registeredViews.TryGetValue(controlId, out var targetView))
            {
                inputManager.ShowSoftInput(targetView, ShowFlags.Forced);
                inputManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.None);
            }
        }


        public void MinimizeMe()
        {
            Task.Run(async () =>
            {
                await Task.Delay(100);
                var main = new Intent(Intent.ActionMain);
                main.AddCategory(Intent.CategoryHome);
                main.SetFlags(ActivityFlags.NewTask);
                StartActivity(main);
            });
        }
    }
}